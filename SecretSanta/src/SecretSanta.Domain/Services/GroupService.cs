using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Domain.Services
{
    public class GroupService
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

        public Group RemoveGroup(Group @group)
        {
            DbContext.Groups.Remove(@group);
            DbContext.SaveChanges();
            return @group;
        }

        public Group UpdateGroup(Group @group)
        {
            DbContext.Groups.Update(@group);
            DbContext.SaveChanges();
            return @group;
        }

        public User AddUserToGroup(int @groupId, User @user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (groupId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(groupId));
            }

            Group foundGroup = DbContext.Groups.Include(g => g.GroupUsers)
                                        .SingleOrDefault(g => g.Id == groupId);

            GroupUser groupUser = new GroupUser {
                                                    GroupId = groupId,
                                                    UserId = user.Id,
                                                    User = user
                                                };

            foundGroup.GroupUsers.Add(groupUser);
            DbContext.SaveChanges();
            return user;
        }

        public User RemoveUserFromGroup(int @groupId, User @user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (groupId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(groupId));
            }

            Group group = DbContext.Groups.Include(g => g.GroupUsers)
                                            .SingleOrDefault(g => g.Id == groupId);
            GroupUser groupUserToRemove = group.GroupUsers.Single(gu => gu.GroupId == groupId 
                                                                    && gu.UserId == user.Id);
            group.GroupUsers.Remove(groupUserToRemove);
            DbContext.SaveChanges();
            return user;
        }

        public List<Group> GetAllGroups()
        {
            return DbContext.Groups.ToList();
        }

        public List<User> GetAllUsersFromGroup(int @groupId)
        {
            return DbContext.Groups
                .Where(x => x.Id == groupId)
                .SelectMany(x => x.GroupUsers)
                .Select(x => x.User)
                .ToList();
        }
    }
}