﻿using MediatR;
using Shared.Dtos;

namespace Application.MediatR.UsersRepository;

public sealed class CreateUserCommand : IRequest<bool>
{
    public UserDto UserDto { get; }

    public CreateUserCommand(UserDto userDto)
    {
        UserDto = userDto;
    }
}