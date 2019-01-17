using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GroupService
    {
        public GroupService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        // TODO: Create group
        
        // TODO: Add users
        
        // TODO: Remove users
    }
}