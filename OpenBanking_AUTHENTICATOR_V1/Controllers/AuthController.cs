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

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action(nameof(GoogleCallback));
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
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

    // Check if user exists, else add
    var user = await _userRepository.GetByGoogleIdAsync(googleId);
    if (user == null)
    {
        user = new User
        {
            GoogleId = googleId,
            Email = email,
            Name = name ?? ""
        };
        await _userRepository.AddAsync(user);
    }

    // Generate JWT token and set it as cookie
    var jwtToken = GenerateJwtToken(user);

    // Set JWT in Cookie
    Response.Cookies.Append("jwt", jwtToken, new CookieOptions
    {
        HttpOnly = true,
        Secure = true, // Only send cookie over HTTPS
        SameSite = SameSiteMode.Lax, // Adjust to your needs
        Expires = DateTime.Now.AddDays(7)
    });

    // Redirect the user to the /dashboard route
    return Redirect("http://localhost:8030/dashboard");  // Assuming your frontend runs on localhost:4200
}


        // Method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.GoogleId),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetByGoogleIdAsync(userId);

            if (user == null)
                return Unauthorized("User not found");

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

    }
}
