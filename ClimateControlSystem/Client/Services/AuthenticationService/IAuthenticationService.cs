using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserDto userForAuthentication);
        Task Logout();
    }
}