using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserDtoModel userForAuthentication);
        Task Logout();
    }
}