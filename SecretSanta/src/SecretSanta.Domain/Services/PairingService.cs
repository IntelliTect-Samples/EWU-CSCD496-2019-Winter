using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext {get;}
        private IRandomService RandomService { get; }

        public PairingService(ApplicationDbContext db, IRandomService randomService = null)
        {
            DbContext = db ?? throw new ArgumentNullException(nameof(db));
            RandomService = randomService ?? new RandomService();
        }

        public async Task<bool> GeneratePairingsForGroup(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .FirstOrDefaultAsync(x => x.Id == groupId);

            var userIds = group.GroupUsers.Select(groupUser => groupUser.UserId).ToList();

            if (userIds == null|| userIds.Count < 2)
            {
                return false;
            }

            Task<List<Pairing>> task = Task.Run(() => GenerateUserPairings(userIds));
            List<Pairing> myPairings = await task;

            await DbContext.Pairings.AddRangeAsync(myPairings);
            await DbContext.SaveChangesAsync();

            return true;

        }

        public async Task<List<Pairing>> GenerateUserPairings(int groupIds)
        {
            var userIds = await DbContext.Groups
                .Where(x => x.Id == groupIds)
                .SelectMany(x => x.GroupUsers, (x, xu) => xu.UserId)
                .ToListAsync();

            List<Pairing> pairings = await Task.Run(() => GenerateUserPairings(userIds));

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();
            return pairings;
        }

        private List<Pairing> GenerateUserPairings(List<int> userIds)
        {
            var pairings = new List<Pairing>();
            var random = RandomService;
            var randUserIds = userIds.OrderBy(id => random.Next()).ToList();

            for (var i = 0; i < randUserIds.Count - 1; i++)
            {
                var paring = new Pairing
                {
                    RecipientId = randUserIds[i],
                    SantaId = randUserIds[i + 1]
                };
                pairings.Add(paring);
            }

            var lastPairing = new Pairing
            {
                SantaId = randUserIds.First(),
                RecipientId = randUserIds.Last()
            };
            pairings.Add(lastPairing);

            return pairings;

        }
    }
}
