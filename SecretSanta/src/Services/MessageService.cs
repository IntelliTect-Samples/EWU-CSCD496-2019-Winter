using Microsoft.EntityFrameworkCore;
using src.Model;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace src.Services
{
    public class MessageService
    {
        private ApplicationDbContext Db { get; }

        public MessageService(ApplicationDbContext db)
        {
            Db = db;
        }

        public void AddMessage(Message message)
        {
            Db.Messages.AddAsync(message).Wait();
            Db.SaveChangesAsync();
        }
    }
}
