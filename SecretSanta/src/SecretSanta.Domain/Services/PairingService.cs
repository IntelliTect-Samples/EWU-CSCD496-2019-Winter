using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class PairingService
    {
        private SecretSantaDbContext DbContext { get; }

        public PairingService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public void CreatePairing(User santa, User recipient)
        {
            Pairing newPair = new Pairing { UserFor = recipient, Santa = santa };

            DbContext.Pairs.Add(newPair);
            DbContext.SaveChanges();
        }

        public Pairing FindPairing(int id)
        {
            return DbContext.Pairs.Find(id);
        }
    }
}
