using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IUserManager
    {
        Task<bool> CreateNewUser(UserDtoModel request);
        Task<string> GetTokenForUser(UserDtoModel request);
    }
}