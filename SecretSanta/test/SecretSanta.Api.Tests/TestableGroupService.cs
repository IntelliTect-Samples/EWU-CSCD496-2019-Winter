using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public List<Group> GroupsToReturn { get; set; }
        public List<User> UsersToReturn { get; set; }
        public Group LastGroupModified;
        public int LastGroupIDModified { get; set; }
        public User LastUserModified { get; set; }

        public Group AddGroup(Group group)
        {
            LastGroupModified = group;
            return group;
        }

        public Group UpdateGroup(Group group)
        {
            LastGroupModified = group;
            return group;
        }

        public Group DeleteGroup(Group group)
        {
            LastGroupModified = group;
            return group;
        }

        public User AddUserToGroup(int groupId, User user)
        {
            LastGroupIDModified = groupId;
            LastUserModified = user;
            return user;
        }

        public User RemoveUserFromGroup(int groupId, User user)
        {
            LastGroupIDModified = groupId;
            LastUserModified = user;
            return user;
        }

        public List<User> FetchAllUsersInGroup(int groupId)
        {
            LastGroupIDModified = groupId;
            return UsersToReturn;
        }


        public List<Group> FetchAll()
        {
            return GroupsToReturn;
        }
    }
}