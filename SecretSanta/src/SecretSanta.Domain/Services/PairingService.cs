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
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<Pairing>> GetPairingsForGroup(int groupId)
        {
            List<Pairing> pairings = await DbContext.Pairings
                .Where(p => p.GroupId == groupId)
                .ToListAsync();

            return pairings;
        }

        public async Task<List<Pairing>> GeneratePairingsForGroup(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(g => g.GroupUsers)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            if (userIds is null || userIds.Count < 2) return null;

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds, groupId));
            List<Pairing> pairings = await task;

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return pairings;
        }

        private List<Pairing> GetPairings(List<int> userIds, int groupId)
        {
            var random = new Random();
            var randomUserIds = new List<int>();
            var pairings = new List<Pairing>();
            int listSize = userIds.Count;

            while (randomUserIds.Count < listSize)
            {
                int index = random.Next(userIds.Count);

                randomUserIds.Add(userIds[index]);
                userIds.Remove(userIds[index]);
            }

            for (int i = 0; i < randomUserIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = randomUserIds[i],
                    RecipientId = randomUserIds[i + 1],
                    GroupId = groupId
                };
                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = randomUserIds.Last(),
                RecipientId = randomUserIds.First(),
                GroupId = groupId
            };

            pairings.Add(lastPairing);

            return pairings;
        }
    }
}
