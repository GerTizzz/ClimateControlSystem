using ClimateControlSystem.Server.Resources.Authentication;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<List<AuthenticatedUserModel>> GetUsers();

        Task<AuthenticatedUserModel> GetUser(int id);

        Task<bool> Create(AuthenticatedUserModel newUser);

        Task<AuthenticatedUserModel?> GetUserByName(string userName);

        Task<bool> UpdateUser(AuthenticatedUserModel updateUser, int id);

        Task<bool> DeleteUser(int id);
    }
}