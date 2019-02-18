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
        public PairingService(ApplicationDbContext dbContext, IRandom random)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Random = random ?? throw new ArgumentNullException(nameof(random));
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
            List<Pairing> pairings = new List<Pairing>().OrderBy(x => Random.Next()).ToList();

            for (var i = 0; i < userIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i + 1],
                    GroupOrigin = groupId
                };
                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First(),
                GroupOrigin = groupId
            };
            pairings.Add(lastPairing);

            return pairings;
        }
    }
}