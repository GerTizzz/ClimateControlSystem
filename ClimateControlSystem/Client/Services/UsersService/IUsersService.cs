using ClimateControl.Shared.Dtos;

namespace ClimateControl.WebClient.Services.UsersService
{
    public interface IUsersService
    {
        Task CreateUser(UserDto user);
        Task DeleteUser(int id);
        Task<UserDto?> GetUser(int id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task UpdateUser(UserDto user);
    }
}