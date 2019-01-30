using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
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

        public void DeleteGroup(Group group)
        {
            DbContext.Groups.Remove(group);
            DbContext.SaveChanges();
        }

        public List<Group> FetchAll()
        {
            return DbContext.Groups.ToList();
        }
    }
}