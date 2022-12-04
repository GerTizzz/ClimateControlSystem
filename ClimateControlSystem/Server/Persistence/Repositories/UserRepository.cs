﻿using ClimateControlSystem.Server.Domain.Repositories;
using ClimateControlSystem.Server.Persistence.Context;
using ClimateControlSystem.Server.Resources.RepositoryResources;
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

        public async Task<List<UserRecord>> GetUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            return users;
        }

        public async Task<UserRecord> GetUser(int id)
        {
            var user = await _context.Users
                .OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<bool> Create(UserRecord newUser)
        {
            if (_context.Users.Any(user => user.Name == newUser.Name))
            {
                return false;
            }

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserRecord> GetUserByName(string userName)
        {
            return await _context.Users.OrderBy(user => user.Id)
                .FirstOrDefaultAsync(user => user.Name == userName);
        }

        public async Task<bool> UpdateUser(UserRecord updateUser, int id)
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