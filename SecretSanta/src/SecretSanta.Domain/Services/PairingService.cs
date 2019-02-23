using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
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
        private ApplicationDbContext DbContext { get; }
        private static readonly System.Random randomGenerator = new System.Random();
        static readonly object _lock = new object();

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Pairing>> GeneratePairing(int groupId)
        {
            if (groupId < 1)
            {
                return null;
            }
            else
            {
                Group group = await DbContext.Groups
                                        .Include(x => x.GroupUsers)
                                        .SingleOrDefaultAsync(x => x.Id == groupId);

                if (group == null)
                {
                    return null;
                }
                else
                {

                    List<int> userIds = group.GroupUsers.Select(groupUser => groupUser.UserId).ToList();

                    if (userIds == null || userIds.Count < 2)
                    {
                        return null;
                    }
                    else
                    {
                        //Invoke getPairings on seperate thread. background thread
                        Task<List<Pairing>> task = Task.Run(() => GetPairings(userIds, groupId));
                        List<Pairing> myPairings = await task;

                        await DbContext.Pairings.AddRangeAsync(myPairings);
                        /* equivalent
                        foreach (Pairing pair in myPairings)
                        {
                            await DbContext.Pairings.AddAsync(pair);
                        }
                        */
                        await DbContext.SaveChangesAsync();

                        return myPairings;
                    }
                }
            }
        }


        private List<Pairing> GetPairings(List<int> userIds, int groupId)
        {
            if (userIds == null)
            {
                throw new ArgumentNullException(nameof(userIds));
            }
            else
            {
                List<Pairing> pairings = new List<Pairing>();
                int originalListSize = userIds.Count;

                List<int> randomOrderUserId = new List<int>();

                //lock (_LockKey); //may not be necessary, but placing it for thread safety in incase of multiple thread reaching the Random.next
                while (randomOrderUserId.Count < originalListSize)
                {
                    Monitor.Enter(_lock); //Alternative way I tried to lock. Trying to use lock (_lockKey), I thought didn't quite work when test
                    int randomGenIndex = randomGenerator.Next(userIds.Count);
                    randomOrderUserId.Add(userIds[randomGenIndex]);
                    userIds.Remove(userIds[randomGenIndex]); //avoid duplicates
                    Monitor.Exit(_lock);
                }

                for (int i = 0; i < randomOrderUserId.Count - 1; i++)
                {
                    Pairing pair = new Pairing
                    {
                        SantaId = randomOrderUserId[i],
                        RecipientId = randomOrderUserId[i + 1],
                        GroupId = groupId
                    };
                    pairings.Add(pair);
                }

                Pairing lastPair = new Pairing
                {
                    SantaId = randomOrderUserId.Last(),
                    RecipientId = randomOrderUserId.First(),
                    GroupId = groupId
                };

                pairings.Add(lastPair);

                return pairings;
            }
        }

        public async Task<List<Pairing>> GetPairingsList(int groupId)
        {
            return await DbContext.Pairings
                        .Where(pair => pair.GroupId == groupId)
                        .ToListAsync();
        }
    }
}
