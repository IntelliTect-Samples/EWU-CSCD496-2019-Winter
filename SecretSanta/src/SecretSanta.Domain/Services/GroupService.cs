using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Group CreateGroup(Group group)
        {
            if (group is null) throw new ArgumentNullException(nameof(group));
            if (group.Id == 0) DbContext.Groups.Add(group);
            DbContext.SaveChanges();
            return group;
        }

        public Group UpdateGroup(Group group)
        {
            if (group is null) throw new ArgumentNullException(nameof(group));
            return UpdateGroup(group, group.Id);
        }

        public Group UpdateGroup(Group group, int groupId)
        {
            if (groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));

            DbContext.Groups.Update(group);
            DbContext.SaveChanges();
            return group;
        }

        public Group DeleteGroup(Group group)
        {
            if (group is null) throw new ArgumentNullException(nameof(group));
            if (group.Id == 0) throw new ArgumentException("Cannot update, group.Id was 0.");

            DbContext.Groups.Remove(group);
            DbContext.SaveChanges();
            return group;
        }

        public Group Find(int id)
        {
            return DbContext.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .SingleOrDefault(g => g.Id == id);
        }

        public User AddUserToGroup(User user, int groupId)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));
            if (groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));

            Group group = Find(groupId);
            
            UserGroups userGroups = new UserGroups
            {
                User = user,
                UserId = user.Id,
                Group = group,
                GroupId = group.Id
            };

            if(!group.UserGroups.Contains(userGroups))
            {
                DbContext.UserGroups.Add(userGroups);
                DbContext.SaveChanges();
            }
            return user;
        }

        public int RemoveUserFromGroup(int groupId, int userId)
        {
            if (userId <= 0 || groupId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            UserGroups userGroups = DbContext.UserGroups.Find(userId, groupId);
            if (userGroups != null)
            {
                DbContext.UserGroups.Remove(userGroups);
                DbContext.SaveChanges();
                return userId;
            }
            return -1;
        }

        public List<Group> GetAllGroups()
        {
            return DbContext.Groups.ToList();
        }
    }
}
