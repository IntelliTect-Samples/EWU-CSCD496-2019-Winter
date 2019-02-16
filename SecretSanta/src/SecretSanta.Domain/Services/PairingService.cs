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

        public async Task<List<Pairing>> GeneratePairingsForGroup(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(g => g.GroupUsers)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            if (userIds is null || userIds.Count < 2) return null;

            Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds));
            List<Pairing> pairings = await task;

            await DbContext.Pairings.AddRangeAsync(pairings);
            await DbContext.SaveChangesAsync();

            return pairings;
        }

        private List<Pairing> GetPairings(List<int> userIds)
        {
            var random = new Random();
            bool noMoreZeros = false;

            int[] isSanta = new int[userIds.Count];
            int[] isRecipient = new int[userIds.Count];
            var pairings = new List<Pairing>();

            while(!noMoreZeros)
            {
                int santaIndex = random.Next(userIds.Count);
                int recipientIndex = random.Next(userIds.Count);

                if (isRecipient[recipientIndex] == 0 && isSanta[santaIndex] == 0 
                        && santaIndex != recipientIndex)
                {
                    var pairing = new Pairing
                    {
                        SantaId = userIds[santaIndex],
                        RecipientId = userIds[recipientIndex]
                    };

                    pairings.Add(pairing);

                    isSanta[santaIndex] = 1;
                    isRecipient[recipientIndex] = 1;
                }

                if(!(isSanta.Contains(0) || isRecipient.Contains(0)))
                {
                    noMoreZeros = true;
                }
            }
            return pairings;
        }
    }
}
