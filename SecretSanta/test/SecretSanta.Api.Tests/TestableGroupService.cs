using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public List<Group> GetListOfGroup_Return { get; set; }

        public Group AddGroup_Return { get; set; }
        public Group AddGroup_Group { get; set; }
        public int AddGroup_GroupId { get; set; }

        public Group RemoveGroup_Return { get; set; }
        public int RemoveGroup_GroupId { get; private set; }
        public Group RemoveGroup_Group { get; set; }

        public Group UpdateGroup_Return { get; set; }
        public int UpdateGroup_GroupId { get; private set; }
        public Group UpdateGroup_Group { get; set; }

        public User AddUserToGroup_Return { get; set; }
        public User AddUserToGroup_User { get; set; }
        public int AddUserToGroup_GroupId { get; set; }

        public User RemoveUserToGroup_Return { get; set; }
        public User RemoveUserToGroup_User { get; set; }
        public int RemoveUserToGroup_GroupId { get; set; }

        public List<Group> GetAllGroups_Return { get; set; }

        public int GetAllUsersFromGroup_GroupId { get; private set; }
        public List<User> GetAllUsersFromGroup_Return { get; private set; }

        public Group AddGroup(int groupId, Group group)
        {
            AddGroup_GroupId = groupId;
            AddGroup_Group = group;
            return AddGroup_Return;
        }

        public Group RemoveGroup(int groupId, Group group)
        {
            RemoveGroup_GroupId = groupId;
            RemoveGroup_Group = group;
            return RemoveGroup_Return;
        }

        public Group UpdateGroup(int groupId, Group group)
        {
            UpdateGroup_GroupId = groupId;
            UpdateGroup_Group = group;
            return UpdateGroup_Return;
        }

        public User AddUserToGroup(int groupId, User user)
        {
            AddUserToGroup_GroupId = groupId;
            AddUserToGroup_User = user;
            return AddUserToGroup_Return;
        }

        public User RemoveUserFromGroup(int groupId, User user)
        {
            RemoveUserToGroup_GroupId = groupId;
            RemoveUserToGroup_User = user;
            return RemoveUserToGroup_Return;
        }
        public List<Group> GetAllGroups()
        {
            return GetAllGroups_Return;
        }
        public List<User> GetAllUsersFromGroup(int groupId)
        {
            GetAllUsersFromGroup_GroupId = groupId;
            return GetAllUsersFromGroup_Return;
        }
    }
}