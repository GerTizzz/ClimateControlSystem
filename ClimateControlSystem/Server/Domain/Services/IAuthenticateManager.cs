using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IAuthenticateManager
    {
        Task<string?> GetTokenForUser(UserDto userToVerify);
    }
}