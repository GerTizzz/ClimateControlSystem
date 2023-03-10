using Shared.Dtos;

namespace WebApi.Infrastructure.Services
{
    public interface IAuthenticateManager
    {
        Task<string?> GetTokenForUser(UserDto userToVerify);
    }
}