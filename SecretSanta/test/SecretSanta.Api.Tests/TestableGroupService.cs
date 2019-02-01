﻿using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Tests
{
    class TestableGroupService : IGroupService
    {
        public Group GetGroup_Return { get; set; }
        public int GetGroup_GroupId { get; set; }

        public List<Group> GetAllGroups_Return { get; set; }
        public bool GetAllGroups_ServiceInvoked = false;

        public List<Group> GetAllGroups()
        {
            GetAllGroups_ServiceInvoked = true;
            return GetAllGroups_Return;
        }

        public Group GetGroup(int groupId)
        {
            GetGroup_GroupId = groupId;
            return GetGroup_Return;
        }

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

        public int DeleteGroup_GroupId { get; set; }

        public void DeleteGroup(int groupId)
        {
            DeleteGroup_GroupId = groupId;
        }

        public int AddUserToGroup_GroupId { get; set; }
        public User AddUserToGroup_User { get; set; }
        public void AddUserToGroup(int groupId, User user)
        {
            AddUserToGroup_GroupId = groupId;
            AddUserToGroup_User = user;
        }

        public int RemoveUserFromGroup_GroupId { get; set; }
        public int RemoveUserFromGroup_UserId { get; set; }

        public void RemoveUserFromGroup(int groupId, int userId)
        {
            RemoveUserFromGroup_GroupId = groupId;
            RemoveUserFromGroup_UserId = userId;
        }

        public List<User> GetAllUsersInGroup_Return { get; set; }
        public int GetAllUsersInGroup_GroupId { get; set; }

        public List<User> GetAllUsersInGroup(int groupId)
        {
            GetAllUsersInGroup_GroupId = groupId;
            return GetAllUsersInGroup_Return;
        }
    }
}
