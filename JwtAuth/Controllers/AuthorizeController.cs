using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private ILogger<AuthorizeController> _logger;
        public AuthorizeController(ILogger<AuthorizeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] string username, [FromForm] string password)
        {
            if (username != "WilsonPan" || password != "123456") return BadRequest("username or password invalide");

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,"WilsonPan"),
                new Claim(JwtRegisteredClaimNames.Email,"wilsonpan@github.com"),
            };
            var key = Encoding.ASCII.GetBytes("1G3l0yYGbOINId3A*ioEi4iyxR7$SPzm");
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: username,
                claims: claims,
                expires: System.DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}