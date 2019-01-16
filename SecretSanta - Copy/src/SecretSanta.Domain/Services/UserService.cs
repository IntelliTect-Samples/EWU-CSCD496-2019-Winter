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
        private ApplicationDbContext DbContext { get; set; }
        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public User UpsertUser(User user)
        {
            if(user.Id == 0)
            {
                DbContext.Users.Add(user);
            }
            else
            {
                DbContext.Users.Update(user);
            }

            DbContext.SaveChanges();
            return user;
        }

        public User Find(int id)
        {
            return DbContext.Users.Include(u => u.Gifts).SingleOrDefault(u => u.Id == id);
        }

        public List<User> FetchAll()
        {
            var userTask = DbContext.Users.ToListAsync();
            userTask.Wait();

            return userTask.Result;
        }
    }
}