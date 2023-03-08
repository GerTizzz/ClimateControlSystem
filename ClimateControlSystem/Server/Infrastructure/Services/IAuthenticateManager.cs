using ClimateControl.Shared.Dtos;

namespace ClimateControl.Server.Infrastructure.Services
{
    public interface IAuthenticateManager
    {
        Task<string?> GetTokenForUser(UserDto userToVerify);
    }
}