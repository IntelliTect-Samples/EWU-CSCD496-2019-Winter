using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteGroup(Group group)
        {
            if (@group == null) throw new ArgumentNullException(nameof(@group));

            DbContext.Groups.Remove(group);
            DbContext.SaveChanges();
        }

        public User AddUserToGroup(int groupId, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            if (groupId <= 0) throw new ArgumentException($"Invalid groupId. {groupId} must be greater than 0");

            Group groupInsertingInto = DbContext.Groups
                .Include(group => group.GroupUsers)
                .SingleOrDefault(group => group.Id == groupId);

            groupInsertingInto?.GroupUsers.Add(new GroupUser
            {
                GroupId = groupId,
                UserId = user.Id,
                User = user
            });

            DbContext.SaveChanges();

            return user;
        }

        public User RemoveUserFromGroup(int groupId, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            if (groupId <= 0) throw new ArgumentException($"Invalid groupId. {groupId} must be greater than 0");

            Group groupRemovingFrom = DbContext.Groups
                .Include(group => group.GroupUsers)
                .SingleOrDefault(group => group.Id == groupId);

            groupRemovingFrom?.GroupUsers
                .Remove(groupRemovingFrom?.GroupUsers
                    .SingleOrDefault(groupUser => groupUser.GroupId == groupId 
                                                  && groupUser.UserId == user.Id));

            return user;
        }
    }
}