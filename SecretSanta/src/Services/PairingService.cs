using Microsoft.EntityFrameworkCore;
using src.Model;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace src.Services
{
    public class PairingService
    {
        private ApplicationDbContext Db { get; }

        public PairingService(ApplicationDbContext db)
        {
            Db = db;
        }

        public bool Add(Pairing pairing)
        {
            if (!IsPairingNull(pairing))
            {
                Db.Pairs.AddAsync(pairing);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Pairing pairing)
        {
            if (!IsPairingNull(pairing))
            {
                Db.Pairs.Update(pairing);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public Pairing Find(int id)
        {
            return Db.Pairs
                .Include(pairing => pairing.Recepiant)
                .Include(pairing => pairing.Santa)
                .SingleOrDefault(pairing => pairing.Id == id);
        }

        public bool IsPairingNull(Pairing pairing)
        {
            return pairing == null;
        }

    }
}
