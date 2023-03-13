using Shared.Dtos;

namespace WebClient.Services.UsersService
{
    public interface IUsersService
    {
        Task CreateUser(UserDto user);
        Task DeleteUser(Guid id);
        Task<UserDto?> GetUser(Guid id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task UpdateUser(UserDto user);
    }
}