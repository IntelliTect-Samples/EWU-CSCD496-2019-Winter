using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public User AddUserToGroup_User { get; set; }
        public int AddUserToGroup_GroupId { get; set; }
        public User AddUserToGroup(User user, int groupId)
        {
            AddUserToGroup_User = user;
            AddUserToGroup_GroupId = groupId;
            return user;
        }

        public Group CreateGroup_Group { get; set; }
        
        public Group CreateGroup(Group group)
        {
            CreateGroup_Group = group;
            return CreateGroup_Group;
        }

        public Group DeleteGroup_Group { get; set; }
        public Group DeleteGroup(Group group)
        {
            DeleteGroup_Group = group;
            return DeleteGroup_Group;
        }

        public List<Group> ToReturn { get; set; }
        public List<Group> GetAllGroups()
        {
            return ToReturn;
        }

        public int RemoveUserFromGroup_UserId { get; set; }
        public int RemoveUserFromGroup(int groupId, int userId)
        {
            RemoveUserFromGroup_UserId = userId;
            return RemoveUserFromGroup_UserId;
        }

        public Group UpdateGroup_Group { get; set; }
        public Group UpdateGroup(Group group)
        {
            UpdateGroup_Group = group;
            return UpdateGroup_Group;
        }
    }
}
