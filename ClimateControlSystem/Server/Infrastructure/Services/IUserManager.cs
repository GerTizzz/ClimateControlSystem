using ClimateControl.Server.Resources.Repository.TablesEntities;
using ClimateControl.Shared.Dtos;

namespace ClimateControl.Server.Infrastructure.Services
{
    public interface IUserManager
    {
        Task<List<UserDto>> GetUsers();

        Task<UserDto?> GetUserById(int id);

        Task<bool> CreateUser(UserDto user);

        Task<UserEntity?> GetUserByName(string name);

        Task<bool> UpdateUser(UserDto user, int id);

        Task<bool> DeleteUser(int id);
    }
}
