using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> GeneratePairing(int groupId)
        {
            //id is primary key
            //singeordefault will throw if 2 or more
            //firstordefault could grab 0 or 1 rows, but don't throw
            Group group = await DbContext.Groups
                                        .Include(x => x.GroupUsers)
                                        .SingleOrDefaultAsync(x => x.Id == groupId);
            //DB context doesn't have a an exposed table for group users
            List<int> userIds = group.GroupUsers.Select(groupUser => groupUser.UserId).ToList();

            if (userIds == null || userIds.Count < 2)
            {
                return false;
            }

            //Invoke getPairings on seperate thread. background thread
            Task <List<Pairing>> task = Task.Run( () => GetPairings(userIds));
            List<Pairing> myPairings = await task;

            await DbContext.Pairings.AddRangeAsync(myPairings);
            /* equivalent
            foreach (Pairing pair in myPairings)
            {
                await DbContext.Pairings.AddAsync(pair);
            }
            */
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
                    RecipientId = userIds[i + 1] 
                };
                pairings.Add(pair);
            }
            Pairing lastPair = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            };
            pairings.Add(lastPair);  

            return pairings;
        }
    }
}
