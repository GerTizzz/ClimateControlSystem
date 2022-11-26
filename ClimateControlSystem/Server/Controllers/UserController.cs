using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Services;
using ClimateControlSystem.Shared;
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

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            var users = await _userManager.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var requiredUser = await _userManager.GetUserById(id);

            if (requiredUser is null)
            {
                return NotFound("Sorry, we have nothing like you are asking!");
            }

            return Ok(requiredUser);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> CreateUser(UserDtoModel user)
        {
            bool hasCreated = await _userManager.CreateUser(user);

            if (hasCreated)
            {
                return Ok(_userManager.GetUsers());
            }

            return BadRequest(_userManager.GetUsers());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<UserModel>>> UpdateUser(UserDtoModel user, int id)
        {
            bool hasUpdated = await _userManager.UpdateUser(user, id);

            if (hasUpdated)
            {
                return Ok(_userManager.GetUsers());
            }

            return BadRequest(_userManager.GetUsers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserModel>>> DeleteSuperHero(int id)
        {
            bool hasDeleted = await _userManager.DeleteUser(id);

            if (hasDeleted)
            {
                return Ok(_userManager.GetUsers());
            }

            return BadRequest(_userManager.GetUsers());
        }
    }
}
