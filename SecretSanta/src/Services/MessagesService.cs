using src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Services
{
    class MessagesService
    {
        private ApplicationDbContext _db { get; }

        public MessagesService(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
