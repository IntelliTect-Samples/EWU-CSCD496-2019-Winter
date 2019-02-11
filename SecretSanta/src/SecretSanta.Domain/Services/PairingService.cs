using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        public ApplicationDbContext DbContext { get; set; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> GeneratePairingsForGroup(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(g => g.GroupUsers)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            if (userIds is null || userIds.Count < 2) return false;

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds));
            List<Pairing> pairings = await task;

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return true;
        }

        private List<Pairing> GetPairings(List<int> userIds)
        {
            var pairings = new List<Pairing>();

            for(int i = 0; i < userIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i + 1]
                    
                };
                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = userIds.First(),
                RecipientId = userIds.Last()
            };
            pairings.Add(lastPairing);

            return pairings;
        }
    }
}
