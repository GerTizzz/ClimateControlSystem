using Shared.Dtos;

namespace WebClient.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<bool> Login(UserDto userForAuthentication);
    Task Logout();
}