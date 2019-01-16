using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    class UserServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public UserServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public User AddUser(User user)
        {
            DbContext.Users.Add(user);
            DbContext.SaveChanges();
            return user;
        }
    }
}
