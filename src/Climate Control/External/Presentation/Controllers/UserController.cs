using Application.MediatR.UserRepository.Create;
using Application.MediatR.UserRepository.Delete;
using Application.MediatR.UserRepository.Read;
using Application.MediatR.UserRepository.Update;
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
            var users = await _mediator.Send(new GetUsersQuery());

            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var requiredUser = await _mediator.Send(new GetUserByIdQuery(id));

            if (requiredUser is null)
            {
                return NotFound("Sorry, we have nothing like you are asking!");
            }

            return Ok(requiredUser);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser(UserDto user)
        {
            var hasCreated = await _mediator.Send(new CreateUserCommand(user));

            return Ok(hasCreated);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateUser(UserDto user)
        {
            var hasUpdated = await _mediator.Send(new UpdateUserCommand(user));

            return Ok(hasUpdated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteUser(Guid id)
        {
            var hasDeleted = await _mediator.Send(new DeleteUserCommand(id));

            return Ok(hasDeleted);
        }
    }
}
