using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using OpenBanking_AUTHENTICATOR_V1.Models;
using OpenBanking_AUTHENTICATOR_V1.Repositories;
using System.Security.Claims;

namespace OpenBanking_AUTHENTICATOR_V1.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            return Ok(new
            {
                Message = "Successfully authenticated",
                User = new { user.Id, user.GoogleId, user.Email, user.Name }
            });
        }
    }
}
