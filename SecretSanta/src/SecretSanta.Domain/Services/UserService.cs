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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            DbContext.Users.Update(user);
            DbContext.SaveChanges();
            return user;
        }

        public List<User> FetchAll()
        {
            return DbContext.Users.ToList();
        }

        public User RemoveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            DbContext.Users.Remove(user);
            DbContext.SaveChanges();
            return user;
        }

    }
}