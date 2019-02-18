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
    public class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext {get;}

        public PairingService(ApplicationDbContext db)
        {
            DbContext = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<bool> createPairing(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .SingleOrDefaultAsync(x => x.Id == groupId);

            var userIds = group.GroupUsers.Select(groupUser => groupUser.UserId).ToList();

            if (userIds == null|| userIds.Count < 2)
            {
                return false;
            }

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds));
            List<Pairing> myPairings = await task;

            await DbContext.Pairings.AddRangeAsync(myPairings);

            await DbContext.SaveChangesAsync();

            return true;

        }

        private List<Pairing> GetPairings(List<int> userIds)
        {
            List<Pairing> pairings = new List<Pairing>();

            for (int i = 0; i < userIds.Count - 1; i++)
            {
                Pairing pair = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i+1]
                };
            }

            Pairing finalPair = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            };

            pairings.Add(finalPair);

            return pairings;


        }
    }
}
