using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UserRepository.Update
{
    public sealed class UpdateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; }

        public UpdateUserCommand(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
