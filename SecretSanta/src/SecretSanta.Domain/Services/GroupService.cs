using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService: IGroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Group AddGroup(Group @group)
        {
            DbContext.Groups.Add(@group);
            DbContext.SaveChanges();
            return @group;
        }

        public Group UpdateGroup(Group @group)
        {
            DbContext.Groups.Update(@group);
            DbContext.SaveChanges();
            return @group;
        }
        public Group RemoveGroup(Group group)
        {
            if (@group == null)
            {
                throw new ArgumentNullException(nameof(@group));
            }

            DbContext.Remove(@group);
            DbContext.SaveChanges();
            return group;
        }


        public List<User> FetchGroupUsers(int groupId)
        {
            if (groupId <= 0)
            {
                throw new ArgumentException("groupId must be larger than 0 in FetchAllGroup");
            }
            return DbContext.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.GroupUsers)
                .Select(g => g.User)
                .ToList();
        }

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }
    }
}