using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Interfaces
{
    public interface IGroupService
    {
        bool CreateGroup(string title);
        bool CreateGroup(Group group);
        bool UpdateGroup(Group group);
        bool AddUser(User user, int id);
        bool AddUser(int uid, int gid);
        bool RemoveUser(User user, int id);
        bool RemoveUser(int uid, int gid);
        bool DeleteGroup(int id);
        Group FindGroup(int id);
        List<User> GetUsersFromGroup(int id);
        List<Group> GetAllGroups();
    }
}
