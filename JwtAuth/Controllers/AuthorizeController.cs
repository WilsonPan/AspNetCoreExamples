using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly JwtSettings _jwtSettings;

        private readonly Random _random = new Random();
        public AuthorizeController(ILogger<AuthorizeController> logger, IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _jwtSettings = jwtSettings?.Value;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] string username, [FromForm] string password)
        {
            // check account
            if (!username.StartsWith("Wilson") || password != "123456") return BadRequest("username or password invalide");

            // define claims
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,"WilsonPan"),
                new Claim(JwtRegisteredClaimNames.Email,"wilsonpan@github.com"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Nonce, GetNonce()),
            };

            // generate token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(5),
                signingCredentials: _jwtSettings.SigningCredentials
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private string GetNonce()
        {
            var bytes = new byte[128];

            _random.NextBytes(bytes);

            return System.Text.Encoding.Default.GetString(bytes.Where(m => m < 128).ToArray());
        }
    }
}