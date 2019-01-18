using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class PairingService
    {
        public PairingService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public Pairing UpsertPairing(Pairing pairing)
        {
            if (pairing.Id == default(int))
                DbContext.Pairings.Add(pairing);
            else
                DbContext.Pairings.Update(pairing);
            DbContext.SaveChanges();

            return pairing;
        }

        public Pairing DeletePairing(Pairing pairing)
        {
            DbContext.Pairings.Remove(pairing);

            DbContext.SaveChanges();

            return pairing;
        }

        public Pairing Find(int id)
        {
            return DbContext.Pairings
                .Include(pairing => pairing.Recipient)
                .Include(pairing => pairing.Santa)
                .SingleOrDefault(pairing => pairing.Id == id);
        }
    }
}