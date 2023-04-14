using Domain.Entities;

namespace Domain.Repositories;

public interface IUsersRepository
{
    Task<List<User>> GetUsers();

    Task<User?> GetUserById(Guid id);

    Task<bool> SaveUser(User newUser);

    Task<User?> GetUserByName(string userName);

    Task<bool> UpdateUser(User userToUpdate);

    Task<bool> DeleteUser(Guid id);
}