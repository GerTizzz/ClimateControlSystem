using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Commands.UserRepository;

public sealed class CreateUserCommand : IRequest<bool>
{
    public UserDto UserDto { get; }

    public CreateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }
}