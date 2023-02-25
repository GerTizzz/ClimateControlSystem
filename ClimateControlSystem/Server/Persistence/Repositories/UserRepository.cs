using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.Repository.TablesEntities;
using Microsoft.EntityFrameworkCore;

namespace ClimateControlSystem.Server.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PredictionsDbContext _context;

        public UserRepository(PredictionsDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            return users;
        }

        public async Task<UserEntity?> GetUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            
            var user = await _context.Users
                .OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<bool> SaveUser(UserEntity newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            
            if (_context.Users.Any(user => user.Name == newUser.Name))
            {
                return false;
            }

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserEntity?> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
            
            return await _context.Users.OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Name == userName);
        }

        public async Task<bool> UpdateUser(UserEntity updateUser, int id)
        {
            if (updateUser is null)
            {
                throw new ArgumentNullException(nameof(updateUser));
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            
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
