using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthController(IUserManager usersRepository)
        {
            _userManager = usersRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(UserDtoModel request)
        {
            var isCreated = await _userManager.CreateNewUser(request);

            if (isCreated)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDtoModel request)
        {
            var token = await _userManager.GetTokenForUser(request);

            if (token is null)
            {
                return BadRequest(string.Empty);
            }

            return Ok(token);
        }
    }
}
