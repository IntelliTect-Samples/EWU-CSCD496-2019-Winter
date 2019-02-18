using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class GroupService : IGroupService
    {
        private ApplicationDbContext DbContext { get; }

        public GroupService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Group> AddGroup(Group group)
        {
            DbContext.Groups.Add(group);
            await DbContext.SaveChangesAsync();
            return group;
        }

        public async Task<Group> GetById(int id)
        {
            return await DbContext.Groups.FindAsync(id);
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            DbContext.Groups.Update(group);
            await DbContext.SaveChangesAsync();
            return group;
        }

        public async Task<List<Group>> FetchAll()
        {
            return await DbContext.Groups.ToListAsync();
        }

        public async Task<List<User>> GetUsers(int groupId)
        {
            return await DbContext.Groups
                .Where(x => x.Id == groupId)
                .SelectMany(x => x.GroupUsers)
                .Select(x => x.User)
                .ToListAsync();
        }

        public async Task<bool> AddUserToGroup(int groupId, int userId)
        {
            Group foundGroup = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (foundGroup == null) return false;

            User foundUser = await DbContext.Users.FindAsync(userId);
            if (foundUser == null) return false;

            var groupUser = new GroupUser { GroupId = foundGroup.Id, UserId = foundUser.Id };
            if (foundGroup.GroupUsers == null)
            {
                foundGroup.GroupUsers = new List<GroupUser>();
            }
            foundGroup.GroupUsers.Add(groupUser);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveUserFromGroup(int groupId, int userId)
        {
            Group foundGroup = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            GroupUser mapping = foundGroup?.GroupUsers.FirstOrDefault(x => x.UserId == userId);

            if (mapping == null) return false;

            foundGroup.GroupUsers.Remove(mapping);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            Group foundGroup = await DbContext.Groups.FindAsync(groupId);

            if (foundGroup != null)
            {
                DbContext.Groups.Remove(foundGroup);
                await DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}