using Shared.Dtos;

namespace WebClient.Services.UsersService
{
    public interface IUsersService
    {
        Task CreateUser(UserDto user);
        Task DeleteUser(string id);
        Task<UserDto?> GetUser(string id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task UpdateUser(UserDto user);
    }
}