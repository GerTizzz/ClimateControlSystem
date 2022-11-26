using ClimateControlSystem.Server.Resources.Authentication;
using ClimateControlSystem.Shared;

namespace ClimateControlSystem.Server.Domain.Services
{
    public interface IUserManager
    {
        Task<List<UserModel>> GetUsers();

        Task<UserModel> GetUserById(int id);

        Task<bool> CreateUser(UserDtoModel user);

        Task<AuthenticatedUserModel> GetUserByName(string name);

        Task<bool> UpdateUser(UserDtoModel user, int id);

        Task<bool> DeleteUser(int id);
    }
}
