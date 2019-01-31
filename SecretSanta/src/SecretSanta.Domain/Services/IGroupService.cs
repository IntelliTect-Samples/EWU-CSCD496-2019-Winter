using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        Group AddGroup(Group @group);
        Group RemoveGroup(Group @group);
        Group UpdateGroup(Group @group);
        User AddUserToGroup(int @groupId, User @user);
        User RemoveUserFromGroup(int @groupId, User @user);
        List<Group> GetAllGroups();
        List<User> GetAllUsersFromGroup(int @groupId);
    }
}
