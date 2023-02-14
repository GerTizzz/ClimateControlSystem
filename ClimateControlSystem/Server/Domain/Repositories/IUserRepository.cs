using ClimateControlSystem.Server.Resources.Repository.TablesEntities;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetUsers();

        Task<UserEntity> GetUser(int id);

        Task<bool> Create(UserEntity newUser);

        Task<UserEntity?> GetUserByName(string userName);

        Task<bool> UpdateUser(UserEntity updateUser, int id);

        Task<bool> DeleteUser(int id);
    }
}