using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public User AddUserToGroup(int groupId, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Group group = DbContext.Groups.Include(g => g.GroupUsers).SingleOrDefault(g => g.Id == groupId);
            GroupUser groupUser = new GroupUser { GroupId = groupId, UserId = user.Id, User = user, Group = group };
            group?.GroupUsers.Add(groupUser);
            DbContext.SaveChanges();
            return user;
        }

        public User RemoveUserFromGroup(int groupId, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            Group group = DbContext.Groups.Include(g => g.GroupUsers).SingleOrDefault(g => g.Id == groupId);
            GroupUser groupUser = group?.GroupUsers.Single(gu => gu.GroupId == groupId && gu.UserId == user.Id);
            group?.GroupUsers.Remove(groupUser);
            DbContext.SaveChanges();
            return user;
        }

        public List<User> FetchAllUsersInGroup(int groupId)
        {
            Group group = DbContext.Groups.Include(g => g.GroupUsers).SingleOrDefault(g => g.Id == groupId);
            return group?.GroupUsers.Select(gu => gu.User).ToList();
        }
    }
}