using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using ClimateControlSystem.Shared.Common;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IUserManager
    {
        Task<List<UserDTO>> GetUsers();

        Task<UserDTO> GetUserById(int id);

        Task<bool> CreateUser(UserDTO user);

        Task<UserEntity> GetUserByName(string name);

        Task<bool> UpdateUser(UserDTO user, int id);

        Task<bool> DeleteUser(int id);
    }
}
