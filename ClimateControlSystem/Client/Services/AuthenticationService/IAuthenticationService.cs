using ClimateControl.Shared.Dtos;

namespace ClimateControl.WebClient.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserDto userForAuthentication);
        Task Logout();
    }
}