using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public interface IUsersService
    {
        List<UserDtoModel> Users { get; }
        Task CreateUser(UserDtoModel user);
        Task DeleteUser(int id);
        Task<UserDtoModel> GetUser(int id);
        Task<List<UserDtoModel>> GetUsers();
        Task UpdateUser(UserDtoModel user);
    }
}