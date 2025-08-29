using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenBanking_AUTHENTICATOR_V1.Dtos;
using OpenBanking_AUTHENTICATOR_V1.Models;
using OpenBanking_AUTHENTICATOR_V1.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OpenBanking_AUTHENTICATOR_V1.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
            private readonly KongService _kongService;

    public AuthController(IUserRepository userRepository, IConfiguration configuration, KongService kongService)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _kongService = kongService;
    }

        [HttpGet("login")]
public IActionResult Login()
{
    // RedirectUri will automatically go to /signin-google
    var properties = new AuthenticationProperties
    {
        RedirectUri = "/"  // or some page you want to redirect after login
    };
    return Challenge(properties, GoogleDefaults.AuthenticationScheme);
}

       [HttpGet("google/callback")]
public async Task<IActionResult> GoogleCallback()
{
    var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    if (!authenticateResult.Succeeded)
        return Unauthorized("Authentication failed.");

    var claims = authenticateResult.Principal?.Claims;

    if (claims == null)
        return Unauthorized("No claims found.");

    var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

    if (string.IsNullOrEmpty(googleId) || string.IsNullOrEmpty(email))
        return BadRequest("Required claims missing.");

    var user = await _userRepository.GetByGoogleIdAsync(googleId);

    if (user == null)
    {
        user = new User
        {
            GoogleId = googleId,
            Email = email,
            Name = name ?? "",
            isActive = true,
            Society_Type = 0
        };
        await _userRepository.AddAsync(user);
    }
    else
    {
        bool needsUpdate = false;
        if (user.Email != email)
        {
            user.Email = email;
            needsUpdate = true;
        }
        if (user.Name != name)
        {
            user.Name = name ?? "";
            needsUpdate = true;
        }
        if (needsUpdate)
            await _userRepository.UpdateAsync(user);
    }

    // Create claims for cookie authentication
    var claimsIdentity = new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.GoogleId),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("UserId", user.Id.ToString())  // important to access later!
    }, CookieAuthenticationDefaults.AuthenticationScheme);

    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    // Sign in user and set the cookie
    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
    {
        IsPersistent = true,
        ExpiresUtc = DateTime.UtcNow.AddDays(7)
    });

    // Optionally: also set your JWT token cookie if needed for client
    var jwtToken = GenerateJwtToken(user);
    Response.Cookies.Append("jwt", jwtToken, new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Lax,
        Expires = DateTime.UtcNow.AddDays(7)
    });

    var userInfo = new UserInfoDto
    {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        isActive = user.isActive,
        Society_Type = user.Society_Type
    };

    return Ok(userInfo);
}


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.GoogleId),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var googleId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(googleId))
                return Unauthorized("User identifier not found in token.");

            var user = await _userRepository.GetByGoogleIdAsync(googleId);

            if (user == null)
                return Unauthorized("User not found.");

            var userInfo = new UserInfoDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                isActive = user.isActive,
                Society_Type = user.Society_Type
            };

            return Ok(userInfo);
        }

         [HttpPut("assign-plan")]
    [Authorize]
    public async Task<IActionResult> AssignPlanValidator([FromBody] PlanValidator planValidator)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            return Unauthorized("User identifier not found in token.");

        if (!Enum.IsDefined(typeof(BuisnessPlan), planValidator.buisnessPlan))
            return BadRequest($"Invalid BuisnessPlan value: {planValidator.buisnessPlan}");

        try
        {
            var apiKey = GenerateApiKey();
            var expiry = DateTime.UtcNow.AddDays(30);

            await _userRepository.AssignPlanValidatorAsync(userId, planValidator, apiKey, expiry);

            // call Kong to create consumer/key/acl/rate limits
            // decide isAdmin flag from plan if needed
            bool isAdmin = planValidator.buisnessPlan == BuisnessPlan.DENIED; // or your logic
            // call Kong (wrap in try/catch so Kong failures don't blow up DB update)
            try
            {
                await _kongService.CreateConsumerWithKeyAclAndRateAsync(userId, apiKey, planValidator.buisnessPlan.ToString(), isAdmin);
            }
            catch (Exception ex)
            {
                    Console.WriteLine($"❌ Kong consumer creation failed for user {userId}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                // log the error. Option: queue retry or mark user as pending in DB.
                // but continue and return success to client since key saved
                // use ILogger to log ex
            }

            return Ok(new { message = $"User {userId} business plan updated to {planValidator.buisnessPlan}", apiKey, expiresAt = expiry });
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"User with Id {userId} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

        [HttpPost("api-key/renew")]
        [Authorize]
        public async Task<IActionResult> RenewApiKey()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("User identifier not found in token.");

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            user.ApiKey = GenerateApiKey();
            user.ApiKeyExpiry = DateTime.UtcNow.AddDays(30);

            await _userRepository.UpdateAsync(user);

            return Ok(new { apiKey = user.ApiKey, expiresAt = user.ApiKeyExpiry });
        }
        private string GenerateApiKey()
        {
            return Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(32));
        }




    }
}
