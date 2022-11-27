using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public interface IUsersService
    {
        List<UserDtoModel> Users { get; set; }

        Task CreateUser(UserDtoModel user);
        Task DeleteUser(int id);
        Task<UserDtoModel> GetUser(int id);
        Task GetUsers();
        Task UpdateUser(UserDtoModel user);
    }
}