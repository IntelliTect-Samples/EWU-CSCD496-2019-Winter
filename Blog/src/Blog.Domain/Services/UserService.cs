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
        private ApplicationDbContext DbContext { get; set; }
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

        public User Find(int id)
        {
            return DbContext.Users.Include(u => u.Posts).SingleOrDefault(u => u.Id == id);
        }
    }
}
