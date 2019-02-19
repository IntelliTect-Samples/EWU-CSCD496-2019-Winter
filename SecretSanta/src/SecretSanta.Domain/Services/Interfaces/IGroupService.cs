using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> FetchAll();
        Task<Group> GetById(int id);

        Task<Group> AddGroup(Group group);

        Task<Group> UpdateGroup(Group group);

        Task<bool> DeleteGroup(int groupId);

        Task<bool> AddUserToGroup(int groupId, int userId);

        Task<bool> RemoveUserFromGroup(int groupId, int userId);
    }
}
