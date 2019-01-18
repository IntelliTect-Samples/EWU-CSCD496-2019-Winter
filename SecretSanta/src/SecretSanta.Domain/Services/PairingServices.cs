using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class PairingServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public PairingServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Pairing AddPairing(User recipient, User santa)
        {
            Pairing p = new Pairing()
            {
                Recipient = recipient,
                Santa = santa
            };

            DbContext.Parings.Add(p);
            DbContext.SaveChanges();
            return p;
        }
    }
}
