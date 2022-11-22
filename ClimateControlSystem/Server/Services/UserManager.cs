using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Services
{
    public class UserManager : IUserManager
    {
        private IUsersRepository _usersRepository;
        private readonly IConfiguration _configuration;

        public UserManager(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
        }

        public async Task CreateDefaultUser()
        {
            await CreateNewUser(new UserDtoModel() { Name = "admin", Password = "admin" });
        }

        public async Task<bool> CreateNewUser(UserDtoModel request)
        {
            if (_usersRepository.GetUserByName(request.Name) is not null)
            {
                return false;
            }

            TokenHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            UserModel newUser = new UserModel();

            newUser.Name = request.Name;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await _usersRepository.Create(newUser);

            return true;
        }

        public async Task<string> GetTokenForUser(UserDtoModel request)
        {
            UserModel user = await CheckIfUserExist(request);

            if (TokenHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt) is false)
            {
                return null;
            }

            string token = string.Empty;

            if (await IsUserVerifyed(request, user))
            {
                token = TokenHelper.CreateToken(user, _configuration.GetSection("AppSettings:Token").Value);
            }

            return token;
        }

        private async Task<UserModel> CheckIfUserExist(UserDtoModel request)
        {
            UserModel user = await _usersRepository.GetUserByName(request.Name);
                
            return user;
        }

        private async Task<bool> IsUserVerifyed(UserDtoModel request, UserModel user)
        {
            if (TokenHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return true;
            }

            return false;
        }
    }
}
