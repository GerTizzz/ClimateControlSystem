using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IAuthenticateManager
    {
        Task<string> GetTokenForUser(UserDtoModel request);
    }
}