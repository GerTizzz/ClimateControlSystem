using ClimateControl.Server.Infrastructure.Services;
using ClimateControl.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControl.Server.Controllers
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
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userManager.GetUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var requiredUser = await _userManager.GetUserById(id);

            if (requiredUser is null)
            {
                return NotFound("Sorry, we have nothing like you are asking!");
            }

            return Ok(requiredUser);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser(UserDto user)
        {
            var hasCreated = await _userManager.CreateUser(user);

            return Ok(hasCreated);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateUser(UserDto user, int id)
        {
            var hasUpdated = await _userManager.UpdateUser(user, id);

            return Ok(hasUpdated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var hasDeleted = await _userManager.DeleteUser(id);

            return Ok(hasDeleted);
        }
    }
}
