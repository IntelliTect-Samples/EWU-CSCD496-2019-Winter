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
        public UserService(ApplicationDbContext context)
        {
            DbContext = context;
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
            return DbContext.Users
                .Include(u => u.Gifts)
                //.Include(u => u.UserGroups)
                //    .ThenInclude(ug => ug.Group)
                .SingleOrDefault(u => u.Id == id);
        }

        public List<User> FetchAll()
        {
            var userTask = DbContext.Users.ToListAsync();
            userTask.Wait();

            return userTask.Result;
        }

        public static User CreateUser(string first, string last)
        {
            return new User { FirstName = first, LastName = last };
        }
    }
}