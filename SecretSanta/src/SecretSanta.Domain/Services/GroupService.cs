using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        // TODO: Wrap this up. Leaving the rest for when we go into many-many relationships
        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public Group UpsertGroup(Group group)
        {
            if (group.Id == default(int))
                DbContext.Groups.Add(group);
            else
                DbContext.Groups.Update(group);
            DbContext.SaveChanges();

            return group;
        }

        // TODO: Find
        
        // TODO: Add users
        
        // TODO: Remove users
    }
}