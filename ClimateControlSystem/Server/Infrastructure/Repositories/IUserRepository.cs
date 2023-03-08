using ClimateControl.Server.Resources.Repository.TablesEntities;

namespace ClimateControl.Server.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetUsers();

        Task<UserEntity?> GetUser(int id);

        Task<bool> SaveUser(UserEntity newUser);

        Task<UserEntity?> GetUserByName(string userName);

        Task<bool> UpdateUser(UserEntity updateUser, int id);

        Task<bool> DeleteUser(int id);
    }
}