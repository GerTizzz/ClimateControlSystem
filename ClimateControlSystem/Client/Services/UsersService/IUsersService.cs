using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public interface IUsersService
    {
        List<UserModel> Users { get; set; }

        Task CreateUser(UserDtoModel user);
        Task DeleteUser(int id);
        Task<UserModel> GetUser(int id);
        Task GetUsers();
        Task UpdateUser(UserDtoModel user);
    }
}