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
        private ApplicationDbContext DbContext { get; }

        public MessageService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Message AddMessage(Message message)
        {
            DbContext.Messages.Add(message);
            DbContext.SaveChanges();

            return message;
        }

        public Message Find (int messageId)
        {
            return DbContext.Messages
                .Include(message => message.ToUser)
                .Include(message => message.FromUser)
                .SingleOrDefault(message => message.Id == messageId);
        }
    }
}
