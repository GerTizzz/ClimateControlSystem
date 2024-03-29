﻿using MediatR;

namespace ClimateControlSystem.Server.Services.MediatR.Commands.UserRepository;

public sealed class DeleteUserCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteUserCommand(int id)
    {
        Id = id;
    }
}