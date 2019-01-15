using src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Services
{
    public class PairingService
    {
        private ApplicationDbContext _db { get; }

        public PairingService(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
