using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    class TestableGroupService : IGroupService
    {
        public Group AddGroup_Group { get; set; }
        public Group ToReturnGroup { get; set; }
        public Group AddGroup(Group group)
        {
            AddGroup_Group = group;
            return ToReturnGroup;
        }

        public List<Group> ToReturnGroupList { get; set; }
        public List<Group> FetchAll()
        {
            return ToReturnGroupList;
        }

        public int FetchAllUsersInGroup_GroupId { get; set; }
        public List<User> ToReturnUserList { get; set; }
        public List<User> FetchGroupUsers(int groupId)
        {
            FetchAllUsersInGroup_GroupId = groupId;
            return ToReturnUserList;
        }

        public Group RemoveGroup_Group { get; set; }
        public Group ToReturnRemoveGroup { get; set; }
        public Group RemoveGroup(Group group)
        {
            RemoveGroup_Group = group;
            return ToReturnRemoveGroup;
        }

        public Group UpdateGroup_Group { get; set; }
        public Group UpdateGroup(Group group)
        {
            UpdateGroup_Group = group;
            return ToReturnGroup;
        }
    }
}
