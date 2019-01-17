using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class UserService
    {
        public UserService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public User UpsertUser(User user)
        {
            if (user.Id == default(int))
                DbContext.Users.Add(user);
            else
                DbContext.Users.Update(user);
            DbContext.SaveChanges();

            return user;
        }

        public User Find(int id)
        {
            return DbContext.Users.Include(u => u.Gifts)
                .SingleOrDefault(u => u.Id == id);
        }

        public List<User> FetchAll()
        {
            var userTask = DbContext.Users.ToListAsync();
            userTask.Wait();

            return userTask.Result;
        }
    }
}