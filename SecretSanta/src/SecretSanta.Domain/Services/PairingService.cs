using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services
{
    public class PairingService : IPairingService
    {
        private readonly object _Locker = new object();
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> GenerateAllPairs(int groupId)
        {
            Group group = await DbContext.Groups.Include(x => x.GroupUsers).FirstOrDefaultAsync(x => x.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            if(userIds == null || userIds.Count < 2)
            {
                return false;
            }

            Task<List<Pairing>> supportTask = Task.Run(() => BuildPairings(userIds));
            List<Pairing> pairings = await supportTask;

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return true;
        }

        private List<Pairing> BuildPairings(List<int> userIds)
        {
            List<Pairing> pairings = new List<Pairing>();
            List<bool> hasPair = new List<bool>(userIds.Capacity);

            Randomizer(ref userIds);

            lock (_Locker)
            {
                for(int index = 0; index < userIds.Count - 1; index++)
                {
                    Pairing pairing = new Pairing
                    {
                        SantaId = userIds[index],
                        RecipientId = userIds[index + 1]
                    };
                    pairings.Add(pairing);
                }

                Pairing lastPair = new Pairing
                {
                    SantaId = userIds.Last(),
                    RecipientId = userIds.First()
                };
                pairings.Add(lastPair);
            }

            return pairings;
        }

        private void Randomizer(ref List<int> list)
        {
            ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
            int size = list.Count;

            while(size > 1)
            {
                size--;
                int randex = random.Value.Next(size + 1);
                int temp = list[randex];
                list[randex] = list[size];
                list[size] = temp; 
            }
        }
    }
}
