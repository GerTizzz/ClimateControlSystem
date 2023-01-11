using ClimateControlSystem.Server.Resources.RepositoryResources;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IUserManager
    {
        Task<List<UserModelWithCredentials>> GetUsers();

        Task<UserModelWithCredentials> GetUserById(int id);

        Task<bool> CreateUser(UserModelWithCredentials user);

        Task<UserRecord> GetUserByName(string name);

        Task<bool> UpdateUser(UserModelWithCredentials user, int id);

        Task<bool> DeleteUser(int id);
    }
}
