﻿using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Services
{
    public sealed class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _userRepository.GetUser(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            var result = users.Select(user => _mapper.Map<UserDto>(user)).ToList();

            return result;
        }

        public Task<bool> CreateUser(UserDto user)
        {
            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = _mapper.Map<UserEntity>(user);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            return _userRepository.Create(newUser);
        }

        public Task<bool> UpdateUser(UserDto user, int id)
        {
            var authUser = _mapper.Map<UserEntity>(user);

            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            authUser.PasswordHash = passwordHash;
            authUser.PasswordSalt = passwordSalt;

            return _userRepository.UpdateUser(authUser, id);
        }

        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public Task<UserEntity?> GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }
    }
}
