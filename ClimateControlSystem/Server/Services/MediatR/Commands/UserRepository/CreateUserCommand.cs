using ClimateControlSystem.Shared.Common;
using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands.UserRepository;

public sealed class CreateUserCommand : IRequest<bool>
{
    public UserDto UserDto { get; }

    public CreateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }
}