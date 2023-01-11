using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateManager _authManager;

        public AuthController(IAuthenticateManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserModelWithCredentials request)
        {
            var token = await _authManager.GetTokenForUser(request);

            if (token is null)
            {
                return BadRequest(string.Empty);
            }

            return Ok(token);
        }
    }
}
