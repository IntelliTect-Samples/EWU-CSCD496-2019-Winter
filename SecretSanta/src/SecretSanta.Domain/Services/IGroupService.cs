using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Group AddGroup(int groupId, Group group);
        Group RemoveGroup(int groupId, Group group);
        Group UpdateGroup(int groupId, Group group);
        User AddUserToGroup(int groupId, User user);
        User RemoveUserFromGroup(int groupId, User user);
        List<Group> GetAllGroups();
        List<User> GetAllUsersFromGroup(int groupId);
    }
}
