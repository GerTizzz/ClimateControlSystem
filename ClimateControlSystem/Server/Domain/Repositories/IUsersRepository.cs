using ClimateControlSystem.Server.Resources.Authentication;

namespace ClimateControlSystem.Server.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> Create(UserModel newUser);
        Task<UserModel?> GetUserByName(string userName);
    }
}