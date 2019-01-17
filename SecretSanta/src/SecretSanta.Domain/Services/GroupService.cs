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

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Group AddGroup(Group group)
        {
            DbContext.Groups.Add(group);
            DbContext.SaveChanges();

            return group;
        }

        public void AddUserToGroup (int groupId, User user)
        {
            // FIXME: uncomment once many-to-many is understood.
            // I think this is how it goes though.
            /*
            var group = DbContext.Groups
                .Include(g => g.Users)
                .SingleOrDefault(g => g.Id == groupId);

            group.Users.Add(user);
            DbContext.SaveChanges();
            */
        }

        public void RemoveUserFromGroup (int groupId, User user)
        {
            // FIXME: Uncomment once many-to-many is understood.
            /*
            var group = DbContext.Groups
                .Include(g => g.Users)
                .SingleOrDefault(g => g.Id == groupId);

            group.Users.Remove(user);
            DbContext.SaveChanges();
            */
        }

        public void Find(int groupId)
        {
            /*
            return DbContext.Groups
                .Include(group => group.Users)
                .SingleOrDefault(Group => Group.Id == groupId);
            */
        }
    }
}
