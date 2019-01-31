using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public interface IGroupService
    {
        List<Group> GetAllGroups();
        User AddUserToGroup(User user, int groupId);
        int RemoveUserFromGroup(int groupId, int userId);
        Group CreateGroup(Group group);
        Group UpdateGroup(Group group);
        int DeleteGroup(int groupId);
    }
}
