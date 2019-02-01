using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Group AddGroup(Group group);
        Group UpdateGroup(Group group);
        Group DeleteGroup(Group group);
        List<Group> FetchAll();
        User AddUserToGroup(int groupId, User user);
        User RemoveUserFromGroup(int groupId, User user);
        List<User> FetchAllUsersInGroup(int groupId);
    }
}
