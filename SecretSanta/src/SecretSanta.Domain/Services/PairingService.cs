using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        public PairingService(ApplicationDbContext dbContext, IRandom random = null)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Random = random ?? new BetterRandom();
        }

        private ApplicationDbContext DbContext { get; }
        private IRandom Random { get; }

        public async Task<List<Pairing>> GeneratePairings(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            var userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();
            if (userIds == null || userIds.Count < 2) return null;

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds, groupId));
            List<Pairing> myPairings = await task;

            await DbContext.Pairings.AddRangeAsync(myPairings);
            await DbContext.SaveChangesAsync();

            return myPairings;
        }

        private List<Pairing> GetPairings(List<int> userIds, int groupId)
        {
            // Leverage Linq to randomize list
            List<int> randomizedUserIds = userIds.OrderBy(x => Random.Next()).ToList();
            
            List<Pairing> pairings = new List<Pairing>();

            for (var i = 0; i < randomizedUserIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = randomizedUserIds[i],
                    RecipientId = randomizedUserIds[i + 1],
                    GroupOrigin = groupId
                };
                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = randomizedUserIds.Last(),
                RecipientId = randomizedUserIds.First(),
                GroupOrigin = groupId
            };
            pairings.Add(lastPairing);

            return pairings;
        }
    }
}