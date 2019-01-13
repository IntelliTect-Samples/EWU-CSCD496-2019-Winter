using Microsoft.EntityFrameworkCore;
using Blog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Domain.Services
{
    public class UserService
    {
        private ApplicationDbContext DbContext { get; }
        public UserService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public void UpsertUser(User user)
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
    }
}
