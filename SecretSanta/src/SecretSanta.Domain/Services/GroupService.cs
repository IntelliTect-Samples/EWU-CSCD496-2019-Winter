using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Group Find(int id)
        {
            return DbContext.Groups.Find(id);
        }

        public Group AddGroup(Group Group)
        {
            DbContext.Groups.Add(Group);
            DbContext.SaveChanges();
            return Group;
        }

        public Group UpdateGroup(Group Group)
        {
            DbContext.Groups.Update(Group);
            DbContext.SaveChanges();
            return Group;
        }

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }

        public List<User> GetUsers(int groupId)
        {
            return DbContext.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.GroupUsers)
                .Select(gu => gu.User)
                .ToList();

        }

        public bool AddUserToGroup(int groupId, int userId)
        {
            Group foundGroup = DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefault(x => x.Id == groupId);
            if (foundGroup == null) return false;

            User foundUser = DbContext.Users.Find(userId);
            if (foundUser == null) return false;

            var groupUser = new GroupUser { GroupId = foundGroup.Id, UserId = foundUser.Id };
            if (foundGroup.GroupUsers == null)
            {
                foundGroup.GroupUsers = new List<GroupUser>();
            }
            foundGroup.GroupUsers.Add(groupUser);
            DbContext.SaveChanges();

            return true;
        }

        public bool RemoveUserFromGroup(int groupId, int userId)
        {
            Group foundGroup = DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefault(x => x.Id == groupId);

            GroupUser mapping = foundGroup?.GroupUsers.FirstOrDefault(x => x.UserId == userId);

            if (mapping == null) return false;

            foundGroup.GroupUsers.Remove(mapping);
            DbContext.SaveChanges();

            return true;
        }

        public bool DeleteGroup(int groupId)
        {
            Group foundGroup = DbContext.Groups.Find(groupId);

            if (foundGroup != null)
            {
                DbContext.Groups.Remove(foundGroup);
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public Group GetGroup(int id)
        {
            return DbContext.Groups.Find(id);
        }
    }
}