using MediatR;
using Shared.Dtos;

namespace WebApi.Services.MediatR.Commands.UserRepository
{
    public sealed class CreateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; }

        public CreateUserCommand(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}