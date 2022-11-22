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

        public async Task<bool> Create(UserModel newUser)
        {
            if (_context.Users.Any(user => user.Name == newUser.Name))
            {
                return false;
            }

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel?> GetUserByName(string userName)
        {
            return await _context.Users.OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Name == userName);
        }
    }
}
