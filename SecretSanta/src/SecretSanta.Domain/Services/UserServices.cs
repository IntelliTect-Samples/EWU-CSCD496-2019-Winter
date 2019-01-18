using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class UserServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public UserServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public User AddUpdateUser(User user)
        {
            if (user.Id == default(int))
                DbContext.Users.Add(user);
            else
                DbContext.Users.Update(user);

            DbContext.SaveChanges();
            return user;
        }

        
    }
}
