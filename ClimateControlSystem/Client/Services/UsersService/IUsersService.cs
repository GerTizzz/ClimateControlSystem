using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Client.Services.UsersService
{
    public interface IUsersService
    {
        List<UserModelWithCredentials> Users { get; }
        Task CreateUser(UserModelWithCredentials user);
        Task DeleteUser(int id);
        Task<UserModelWithCredentials> GetUser(int id);
        Task<List<UserModelWithCredentials>> GetUsers();
        Task UpdateUser(UserModelWithCredentials user);
    }
}