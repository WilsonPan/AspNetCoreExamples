using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<AuthorizeController> _logger;
        private readonly JwtSettings _jwtSettings;
        public AuthorizeController(ILogger<AuthorizeController> logger, IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _jwtSettings = jwtSettings?.Value;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] string username, [FromForm] string password)
        {
            if (!username.StartsWith("Wilson") || password != "123456") return BadRequest("username or password invalide");

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"WilsonPan"),
                new Claim(JwtRegisteredClaimNames.Email,"wilsonpan@github.com"),
                new Claim(JwtRegisteredClaimNames.Nonce,System.Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId,"Wilson Id"),
                new Claim(JwtRegisteredClaimNames.UniqueName,"Wilson Pan"),
            };
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: username,
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(5),
                signingCredentials: _jwtSettings.SigningCredentials
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}