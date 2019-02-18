using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext { get; }
        private Random Random { get; } = new Random();
        private readonly object LockKey = new object(); 


        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<List<Pairing>> GeneratePairings(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();
            if(userIds == null || userIds.Count < 2)
            {
                return null;
            }

            List<Pairing> pairings = await Task.Run( () => GetPairings(userIds));
            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return pairings;
        }

        private async Task<List<Pairing>> GetPairings(List<int> userIds)
        {
            var pairings = new List<Pairing>();
            var assignedRecipients = new List<int>();

            foreach (int userId in userIds)
            {
                int indexRecipient = userId;

                //no person can be their own santa && each recipient has only one santa
                while (userIds[indexRecipient] == userId && !assignedRecipients.Contains(indexRecipient) )    
                    indexRecipient = GetIndexLocked() % userIds.Count();

                assignedRecipients.Add(indexRecipient);

                var pairing = new Pairing
                {
                    SantaId = userId,
                    RecipientId = userIds[indexRecipient]
                };
                pairings.Add(pairing);
            }

            return pairings;
        }

        private int GetIndexLocked()
        {
            lock (LockKey)
            {
                return Random.Next();
            }
        }

    }
}
