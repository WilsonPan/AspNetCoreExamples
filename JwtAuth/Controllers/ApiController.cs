using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JwtAuth.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                User.Identity.IsAuthenticated,
                User.Identity.Name,
                Email = User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Email)?.Value
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}