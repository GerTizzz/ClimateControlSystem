using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModelWithCredentials>>> GetUsers()
        {
            var users = await _userManager.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserModelWithCredentials>> GetUser(int id)
        {
            var requiredUser = await _userManager.GetUserById(id);

            if (requiredUser is null)
            {
                return NotFound("Sorry, we have nothing like you are asking!");
            }

            return Ok(requiredUser);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModelWithCredentials>>> CreateUser(UserModelWithCredentials user)
        {
            bool hasCreated = await _userManager.CreateUser(user);

            var result = await _userManager.GetUsers();

            if (hasCreated)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<UserModelWithCredentials>>> UpdateUser(UserModelWithCredentials user, int id)
        {
            bool hasUpdated = await _userManager.UpdateUser(user, id);

            var result = await _userManager.GetUsers();

            if (hasUpdated)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserModelWithCredentials>>> DeleteUser(int id)
        {
            bool hasDeleted = await _userManager.DeleteUser(id);

            var result = await _userManager.GetUsers();

            if (hasDeleted)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
