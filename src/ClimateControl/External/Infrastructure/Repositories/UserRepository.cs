using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ForecastDbContext _context;

        public UserRepository(ForecastDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var user = await _context.Users
                .OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<bool> SaveUser(User newUser)
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

        public async Task<User?> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return await _context.Users.OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Name == userName);
        }

        public async Task<bool> UpdateUser(User userToUpdate)
        {
            if (userToUpdate is null)
            {
                throw new ArgumentNullException(nameof(userToUpdate));
            }

            var requiredUser = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == userToUpdate.Id);

            if (requiredUser is null)
            {
                return false;
            }

            requiredUser.Name = userToUpdate.Name;
            requiredUser.Role = userToUpdate.Role;
            requiredUser.PasswordHash = userToUpdate.PasswordHash;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(Guid id)
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
