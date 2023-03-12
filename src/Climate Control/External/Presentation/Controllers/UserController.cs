using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _mediator.GetUsers();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:string}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var requiredUser = await _mediator.GetUserById(id);

            if (requiredUser is null)
            {
                return NotFound("Sorry, we have nothing like you are asking!");
            }

            return Ok(requiredUser);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser(UserDto user)
        {
            var hasCreated = await _mediator.CreateUser(user);

            return Ok(hasCreated);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser(UserDto user)
        {
            var hasUpdated = await _mediator.UpdateUser(user);

            return Ok(hasUpdated);
        }

        [HttpDelete("{id:string}")]
        public async Task<ActionResult<bool>> DeleteUser(string id)
        {
            var hasDeleted = await _mediator.DeleteUser(id);

            return Ok(hasDeleted);
        }
    }
}
