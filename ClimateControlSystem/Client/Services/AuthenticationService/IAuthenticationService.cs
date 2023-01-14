using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserModelWithCredentials userForAuthentication);
        Task Logout();
    }
}