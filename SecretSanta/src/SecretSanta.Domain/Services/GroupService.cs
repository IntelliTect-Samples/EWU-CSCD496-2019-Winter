using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Group UpsertGroup(Group group)
        {
            if (group.Id == 0)
            {
                DbContext.Groups.Add(group);
            }
            else
            {
                DbContext.Groups.Update(group);
            }
            DbContext.SaveChanges();
            return group;
        }

        public Group Find(int id) // what does this return if nothing is found?
        {
            return DbContext.Groups
                .Include(g => g.UserGroups)
                    .ThenInclude(ug => ug.User)
                .SingleOrDefault(g => g.Id == id);
        }

        public void AddUserToGroup(User user, int groupId)
        {
            if(user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Group group = Find(groupId);
            
            if(group != null)
            {
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
            }
        }

        public void RemoveUserFromGroup(int groupId, int userId)
        {
            Group group = Find(groupId);

            if(group != null)
            {
                UserGroups userGroups = DbContext.UserGroups.Find(userId, groupId);
                if (userGroups != null)
                {
                    DbContext.UserGroups.Remove(userGroups);
                    DbContext.SaveChanges();
                }
            }
        }
    }
}
