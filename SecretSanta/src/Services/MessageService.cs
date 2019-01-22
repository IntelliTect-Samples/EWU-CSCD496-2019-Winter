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

        public bool Add(Message message)
        {
            if (!IsMessageNull(message))
            {
                Db.Messages.AddAsync(message).Wait();
                Db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public bool Update(Message message)
        {
            if (!IsMessageNull(message))
            {
                Db.Messages.Update(message);
                Db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Message Find(int id)
        {
            return Db.Messages
                .Include(message => message.Pairing)
                .SingleOrDefault(message => message.Id == id);
        }

        public void StoreMessage(Pairing pair, Message message)
        {
            pair.Messages.Add(message);
            Db.SaveChanges();
        }

        public bool IsMessageNull(Message message)
        {
            return message == null;
        }



    }
}
