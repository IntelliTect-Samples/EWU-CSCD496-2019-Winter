using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        List<Group> FetchAll();

        Group AddGroup(Group group);

        Group UpdateGroup(Group group);

        bool DeleteGroup(int groupId);

        bool AddUserToGroup(int groupId, int userId);

        bool RemoveUserFromGroup(int groupId, int userId);
    }
}