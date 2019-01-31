using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private ApplicationDbContext DbContext { get; }

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

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }

        public List<User> GetUsers(int groupId)
        {
            return DbContext.Groups
                .Where(x => x.Id == groupId)
                .SelectMany(x => x.GroupUsers)
                .Select(x => x.User)
                .ToList();
        }
    }
}