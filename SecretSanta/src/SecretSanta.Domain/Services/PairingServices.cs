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

        public Pairing AddPairing(Pairing paring)
        {
            
            DbContext.Parings.Add(paring);
            DbContext.SaveChanges();
            return paring;
        }
    }
}
