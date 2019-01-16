using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class UserService
    {
        public UserService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public void AddUser(User user)
        {
            if (user.Id == default(int))
                DbContext.Users.Add(user);
            else
                DbContext.Users.Update(user);
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