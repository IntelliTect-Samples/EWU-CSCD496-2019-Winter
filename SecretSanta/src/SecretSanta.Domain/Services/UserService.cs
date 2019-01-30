using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
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

        public User CreateUser(User user)
        {
            if(user.Id == 0)
            {
                DbContext.Users.Add(user);
                DbContext.SaveChanges();
            }
            return user;
        }

        public User UpdateUser(User updatedUser, int userId)
        {
            User user = Find(userId);
            if(user != null && userId != 0)
            {
                user = updatedUser;
                DbContext.Users.Update(user);
                DbContext.SaveChanges();
                return user;
            }
            return updatedUser;
        }

        public User DeleteUser(int userId)
        {
            User user = Find(userId);
            if(user != null)
            {
                DbContext.Users.Remove(user);
            }
            return user; // make sure this deletes users from groups
        }

        public User Find(int id)
        {
            return DbContext.Users
                .Include(u => u.Gifts)
                .Include(u => u.UserGroups)
                    .ThenInclude(ug => ug.Group)
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

        public List<User> GetUsersForGroup(int groupId)
        {
            if (groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));

            Group group = DbContext.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .SingleOrDefault(g => g.Id == groupId);

            return group.UserGroups.Select(ug => ug.User).ToList();
        }
    }
}