using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    public class TestableGroupService : IGroupService
    {
        public Group ToReturnFindGroup { get; set; }
        public List<Group> ToReturnGetAllGroup { get; set; }
        public List<User> ToReturnGetUsersFromGroup { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public bool AddUser(User user, int id)
        {
            UserId = user.Id;
            GroupId = id;
            return true;
        }

        public bool AddUser(int uid, int gid)
        {
            UserId = uid;
            GroupId = gid;
            return true;
        }

        public bool CreateGroup(string title)
        {
            return true;
        }

        public bool CreateGroup(Group group)
        {
            GroupId = group.Id;
            return true;
        }

        public bool DeleteGroup(int id)
        {
            GroupId = id;
            return true;
        }

        public Group FindGroup(int id)
        {
            GroupId = id;
            ToReturnFindGroup = new Group() { Id = id };

            return ToReturnFindGroup;
        }

        public List<Group> GetAllGroups()
        {
            return ToReturnGetAllGroup;
        }

        public List<User> GetUsersFromGroup(int id)
        {
            GroupId = id;
            return ToReturnGetUsersFromGroup;
        }

        public bool RemoveUser(User user, int id)
        {
            UserId = user.Id;
            GroupId = id;
            return true;
        }

        public bool RemoveUser(int uid, int gid)
        {
            UserId = uid;
            GroupId = gid;
            return true;
        }

        public bool UpdateGroup(Group group)
        {
            GroupId = group.Id;
            return true;
        }
    }
}
