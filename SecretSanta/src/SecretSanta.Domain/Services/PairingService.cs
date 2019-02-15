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
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Pairing>> GeneratePairings(int groupId)
        {
            Group group = await DbContext.Groups
                .Include(x => x.GroupUsers)
                .SingleOrDefaultAsync(x => x.Id == groupId);

            List<int> userIds = group?.GroupUsers?.Select(x => x.UserId).ToList();

            List<Pairing> pairings = new List<Pairing>();
            if (userIds.Count > 1)
            {
                pairings = await Task.Run(() => GetPairingsSingleLoop(userIds));
                await DbContext.Pairings.AddRangeAsync(pairings);
                await DbContext.SaveChangesAsync();
            }

            return pairings;
        }

        private List<Pairing> GetPairingsSingleLoop(List<int> userIds)
        {
            List<Pairing> pairings = new List<Pairing>();

            for (int i = 0; i < userIds.Count - 1; i++)
            {
                pairings.Add(new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i + 1]
                });
            }

            pairings.Add(new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            });

            return pairings;
        }
    }
}
