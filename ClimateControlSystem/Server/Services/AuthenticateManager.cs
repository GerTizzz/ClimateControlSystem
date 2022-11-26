using ClimateControlSystem.Server.Domain.Services;
using ClimateControlSystem.Server.Resources;
using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Services
{
    public class AuthenticateManager : IAuthenticateManager
    {
        private IUserManager _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticateManager(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GetTokenForUser(UserDtoModel request)
        {
            AuthenticatedUserModel user = await _userManager.GetUserByName(request.Name);

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

        private async Task<bool> IsUserVerifyed(UserDtoModel request, AuthenticatedUserModel user)
        {
            if (TokenHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return true;
            }

            return false;
        }
    }
}
