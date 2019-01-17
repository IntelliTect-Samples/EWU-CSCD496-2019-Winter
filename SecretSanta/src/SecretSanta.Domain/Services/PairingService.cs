using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class PairingService
    {
        private ApplicationDbContext DbContext { get; }

        public PairingService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Pairing AddPairing(Pairing pairing)
        {
            DbContext.Pairings.Add(pairing);
            DbContext.SaveChanges();

            return pairing;
        }

        public Pairing Find (int pairingId)
        {
            return DbContext.Pairings
                .Include(pairing => pairing.Recipient)
                .Include(pairing => pairing.Santa)
                .SingleOrDefault(pairing => pairing.Id == pairingId);
        }
    }
}
