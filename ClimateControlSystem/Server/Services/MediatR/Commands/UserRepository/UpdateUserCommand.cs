using ClimateControl.Shared.Dtos;
using MediatR;

namespace ClimateControl.Server.Services.MediatR.Commands.UserRepository;

public sealed class UpdateUserCommand : IRequest<bool>
{
    public UserDto UserDto { get; }
    public int Id { get; }

    public UpdateUserCommand(UserDto userDto, int id)
    {
        UserDto = userDto;
        Id = id;
    }
}