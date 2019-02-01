using System.Collections.Generic;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public Group AddGroup_Group { get; set; }
        public Group AddGroup_Return { get; set; }
        public Group AddGroup(Group @group)
        {
            AddGroup_Group = group;

            return AddGroup_Return;
        }

        public Group UpdateGroup_Group { get; set; }
        public Group UpdateGroup_Return { get; set; }
        public Group UpdateGroup(Group @group)
        {
            UpdateGroup_Group = group;

            return UpdateGroup_Return;
        }

        public List<Group> FetchAll_Return { get; set; }
        public List<Group> FetchAll()
        {
            return FetchAll_Return;
        }

        public int GetUsers_GroupId { get; set; }
        public List<User> GetUsers_Return { get; set; }
        public List<User> GetUsers(int groupId)
        {
            GetUsers_GroupId = groupId;

            return GetUsers_Return;
        }

        public Group DeleteGroup_Group { get; set; }
        public void DeleteGroup(Group @group)
        {
            DeleteGroup_Group = group;
        }

        public int AddUserToGroup_GroupID { get; set; }
        public User AddUserToGroup_User { get; set; }
        public User AddUserToGroup_Return { get; set; }
        public User AddUserToGroup(int groupId, User user)
        {
            AddUserToGroup_GroupID = groupId;
            AddUserToGroup_User = user;

            return AddUserToGroup_Return;
        }

        public int RemoveUserFromGroup_GroupId { get; set; }
        public User RemoveUserFromGroup_User { get; set; }
        public User RemoveUserFromGroup_Return { get; set; }
        public User RemoveUserFromGroup(int groupId, User user)
        {
            RemoveUserFromGroup_GroupId = groupId;
            RemoveUserFromGroup_User = user;

            return RemoveUserFromGroup_Return;
        }
    }
}