using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public interface IUsersService
    {
        List<UserDTO> Users { get; }
        Task CreateUser(UserDTO user);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<List<UserDTO>> GetUsers();
        Task UpdateUser(UserDTO user);
    }
}