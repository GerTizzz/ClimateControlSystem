using AutoMapper;
using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Server.Resources.RepositoryResources;
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

        public async Task<UserModelWithCredentials> GetUserById(int id)
        {
            var user = await _userRepository.GetUser(id);

            return _mapper.Map<UserModelWithCredentials>(user);
        }

        public async Task<List<UserModelWithCredentials>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            var result = users.Select(user => _mapper.Map<UserModelWithCredentials>(user)).ToList();

            return result;
        }

        public Task<bool> CreateUser(UserModelWithCredentials user)
        {
            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserRecord newUser = _mapper.Map<UserRecord>(user);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            return _userRepository.Create(newUser);
        }

        public Task<bool> UpdateUser(UserModelWithCredentials user, int id)
        {
            UserRecord authUser = _mapper.Map<UserRecord>(user);

            TokenHelper.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            authUser.PasswordHash = passwordHash;
            authUser.PasswordSalt = passwordSalt;

            return _userRepository.UpdateUser(authUser, id);
        }

        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public Task<UserRecord> GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }
    }
}
