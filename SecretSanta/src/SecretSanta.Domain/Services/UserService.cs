using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public User AddUser(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            DbContext.SaveChanges();
            return user;
        }

        public bool DeleteUser(User user)
        {
            if (DbContext.Users.Contains(user))
            {
                DbContext.Users.Remove(user);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public List<User> FetchAll()
        {
            return DbContext.Users.ToList();
        }
    }
}