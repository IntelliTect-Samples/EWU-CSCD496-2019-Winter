using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
    {
        private SecretSantaDbContext DbContext { get; }

        public UserService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public bool UpsertUser(User user)
        {
            if (user.Id == default(int))
            {
                DbContext.Users.Add(user);
            }
            else
            {
                DbContext.Users.Update(user);
            }
            DbContext.SaveChanges();
            return true;
        }

        public User Find(int id)
        {
            return DbContext.Users.Find(id);
        }

        public List<User> FetchAll()
        {
            var userTask = DbContext.Users.ToListAsync();
            userTask.Wait();

            return userTask.Result;
        }

        public bool DeleteUser(int id)
        {
            bool test = false;

            User user = Find(id);

            if (user != null)
            {
                DbContext.Remove(user);
                DbContext.SaveChanges();
                test = true;
            }

            return test;
        }
    }
}
