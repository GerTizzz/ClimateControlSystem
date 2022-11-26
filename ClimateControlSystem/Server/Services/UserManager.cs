using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControlSystem.Server.Services
{
    public sealed class UserManager : IUserManager
    {
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUsersRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userRepository.GetUser(id);

            return _mapper.Map<UserModel>(user);
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            return users.Select(user => _mapper.Map<UserModel>(user)).ToList();
        }

        public Task<bool> CreateUser(UserDtoModel user)
        {
            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            AuthenticatedUserModel newUser = _mapper.Map<AuthenticatedUserModel>(user);

            newUser.Name = user.Name;
            newUser.Role = user.Role;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            return _userRepository.Create(newUser);
        }

        public Task<bool> UpdateUser(UserDtoModel user, int id)
        {
            AuthenticatedUserModel authUser = _mapper.Map<AuthenticatedUserModel>(user);

            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            authUser.PasswordHash = passwordHash;
            authUser.PasswordSalt = passwordSalt;

            return _userRepository.UpdateUser(authUser, id);
        }

        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public Task<AuthenticatedUserModel> GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }
    }
}
