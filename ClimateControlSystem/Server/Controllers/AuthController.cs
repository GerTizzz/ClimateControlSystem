using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared;
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

        //[HttpPost("register")]
        //public async Task<ActionResult<bool>> Register(UserDtoModel request)
        //{
        //    var isCreated = await _authManager.Create(request);

        //    if (isCreated)
        //    {
        //        return Ok();
        //    }

        //    return BadRequest();
        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDtoModel request)
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
