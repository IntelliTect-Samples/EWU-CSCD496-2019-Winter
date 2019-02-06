using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public User Find(int id)
        {
            return DbContext.Users.Find(id);
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

        public List<User> FetchAll()
        {
            return DbContext.Users.ToList();
        }

        public bool DeleteUser(int userId)
        {
            var userToDelete = DbContext.Users.SingleOrDefault(u => u.Id == userId);

            if (userToDelete == null)
            {
                throw new InvalidOperationException("UserId does not exist");
            }

            DbContext.Users.Remove(userToDelete);
            return (DbContext.SaveChanges() == 1);
        }

        public User GetUser(int id)
        {
            return DbContext.Users.Find(id);
        }
    }
}