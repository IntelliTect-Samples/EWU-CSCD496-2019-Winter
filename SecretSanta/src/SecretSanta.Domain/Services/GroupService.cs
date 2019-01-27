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

        public UserGroups RemoveUserFromGroup(int groupId, int userId)
        {
            Group group = Find(groupId);
            UserGroups userGroups = null;

            if(group != null)
            {
                foreach(var ug in group.UserGroups
                    .Where(ug => ug.UserId == userId))
                {
                    if(group.UserGroups.Remove(ug))
                    {
                        DbContext.SaveChanges();
                        return ug;
                    }
                }
                    
            }
            return userGroups;
        }
    }
}
