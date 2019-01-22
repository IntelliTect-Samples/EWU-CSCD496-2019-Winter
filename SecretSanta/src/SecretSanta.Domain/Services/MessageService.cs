using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class MessageService
    {
        private ApplicationDbContext DbContext { get; set; }

        public MessageService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Message UpsertMessage(Message message)
        {
            if(message.Id == 0)
            {
                DbContext.Messages.Add(message);
            }
            else
            {
                DbContext.Messages.Update(message);
            }
            DbContext.SaveChanges();
            return message;
        }

        public Message Find(int id)
        {
            return DbContext.Messages
                .Include(m => m.Pairing)
                .SingleOrDefault(m => m.Id == id);
        }

        public Pairing StoreMessage(Pairing pairing, Message message)
        {
            pairing.Messages.Add(message);
            return pairing;
        }

    }
}
