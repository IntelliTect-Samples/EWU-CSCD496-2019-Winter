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
        private ApplicationDbContext DbContext { get; set; }

        public PairingService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Pairing UpsertPairing(Pairing pairing)
        {
            if(pairing.Id == 0)
            {
                DbContext.Pairings.Add(pairing);
            }
            else
            {
                DbContext.Pairings.Update(pairing);
            }
            DbContext.SaveChanges();
            return pairing;
        }

        public Pairing Find(int id)
        {
            return DbContext.Pairings
                .Include(p => p.Santa)
                .Include(p => p.Recipient)
                .Include(p => p.Messages)
                .SingleOrDefault(p => p.Id == id);
        }
    }
}
