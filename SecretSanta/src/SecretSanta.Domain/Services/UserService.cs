using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> AddUser(User user)
        {
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int id)
        {
            return await DbContext.Users.FindAsync(id);
        }

        public async Task<List<User>> FetchAll()
        {
            return await DbContext.Users.ToListAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            User foundUser = await DbContext.Users.FindAsync(userId);

            if (foundUser != null)
            {
                DbContext.Users.Remove(foundUser);
                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}