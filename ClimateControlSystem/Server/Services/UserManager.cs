﻿using ClimateControl.Server.Resources.Repository.TablesEntities;
using ClimateControl.Server.Services.MediatR.Commands.UserRepository;
using ClimateControl.Server.Services.MediatR.Queries.UserRepository;
using MediatR;
using ClimateControl.Shared.Dtos;
using ClimateControl.Server.Infrastructure.Services;

namespace ClimateControl.Server.Services
{
    public sealed class UserManager : IUserManager
    {
        private readonly IMediator _mediator;

        public UserManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var userDto = await _mediator.Send(new GetUserByIdQuery(id));

            return userDto;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _mediator.Send(new GetUsersQuery());

            return users;
        }

        public async Task<bool> CreateUser(UserDto user)
        {
            var result = await _mediator.Send(new CreateUserCommand(user));

            return result;
        }

        public async Task<bool> UpdateUser(UserDto user, int id)
        {
            var result = await _mediator.Send(new UpdateUserCommand(user, id));

            return result;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return result;
        }

        public async Task<UserEntity?> GetUserByName(string name)
        {
            var userEntity = await _mediator.Send(new GetUserByNameQuery(name));

            return userEntity;
        }
    }
}
