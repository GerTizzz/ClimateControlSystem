using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PredictionsDbContext _context;

        public UsersRepository(PredictionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthenticatedUserModel>> GetUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            return users;
        }

        public async Task<AuthenticatedUserModel> GetUser(int id)
        {
            var user = await _context.Users
                .OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<bool> Create(AuthenticatedUserModel newUser)
        {
            if (_context.Users.Any(user => user.Name == newUser.Name))
            {
                return false;
            }

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthenticatedUserModel> GetUserByName(string userName)
        {
            return await _context.Users.OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Name == userName);
        }

        public async Task<bool> UpdateUser(AuthenticatedUserModel updateUser, int id)
        {
            var requiredUser = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (requiredUser is null)
            {
                return false;
            }

            requiredUser.Name = updateUser.Name;
            requiredUser.Role = updateUser.Role;
            requiredUser.PasswordHash = updateUser.PasswordHash;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var requiredUser = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == id);

            if (requiredUser is null)
            {
                return false;
            }

            _context.Remove(requiredUser);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
