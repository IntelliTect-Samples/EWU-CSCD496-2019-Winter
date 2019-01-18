using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public GroupServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Group AddGroup(Group group)
        {
            DbContext.Groups.Add(group);
            DbContext.SaveChanges();
            return group;
        }

        public User AddUserToGroup(UserGroup userGroup)
        {
            DbContext.Groups.Find(userGroup.Group).UserGroups.Add(userGroup);
            DbContext.SaveChanges();
            return userGroup.User;
        }

        public User RemoveUserFromGroup(UserGroup userGroup)
        {
            DbContext.Groups.Find(userGroup.Group).UserGroups.Remove(userGroup);
            DbContext.SaveChanges();
            return userGroup.User;
        }
    }
}
