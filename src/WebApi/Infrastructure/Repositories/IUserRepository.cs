using WebApi.Resources.Repository.TablesEntities;

namespace WebApi.Infrastructure.Repositories
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