using SecretSanta.Domain.Models;
using System;

namespace SecretSanta.Domain.Services
{
    public class PairingService : ApplicationService
    {
        public PairingService(ApplicationDbContext dbContext) : base(dbContext) { }
        
        public Pairing CreatePairing(Group group)
        {
            int p1 = getRandomInt(group.Members.Count);
            int p2 = getRandomInt(group.Members.Count);
            if (p1 != p2)
                return new Pairing { Recipient = group.Members[p1], Santa = group.Members[p2] };
            else
                return CreatePairing(group);
        }

        private int getRandomInt(int memberCount)
        {
            Random rand = new Random();
            return rand.Next(memberCount);
        }

        public Pairing AddPairing(Pairing pairing)
        {
            DbContext.Pairings.Add(pairing);
            DbContext.SaveChanges();

            return pairing;
        }
    }
}