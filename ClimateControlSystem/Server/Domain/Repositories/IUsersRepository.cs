using ClimateControlSystem.Server.Resources.RepositoryResources;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<List<UserRecord>> GetUsers();

        Task<UserRecord> GetUser(int id);

        Task<bool> Create(UserRecord newUser);

        Task<UserRecord?> GetUserByName(string userName);

        Task<bool> UpdateUser(UserRecord updateUser, int id);

        Task<bool> DeleteUser(int id);
    }
}