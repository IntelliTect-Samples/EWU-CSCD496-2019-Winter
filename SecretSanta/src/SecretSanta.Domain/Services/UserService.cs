using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class UserService
    {
        private ApplicationDbContext DbContext { get; }

        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
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

        public User Find (int id)
        {
            return DbContext.Users.Include(user => user.Groups).SingleOrDefault(user => user.Id == id);
        }
    }
}
