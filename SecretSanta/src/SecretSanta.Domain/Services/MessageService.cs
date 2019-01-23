using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class MessageService
    {
        public MessageService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public Message UpsertMessage(Message message)
        {
            if (message.Id == default(int))
                DbContext.Messages.Add(message);
            else
                DbContext.Messages.Update(message);
            DbContext.SaveChanges();

            return message;
        }

        public Message DeleteMessage(Message toDelete)
        {
            DbContext.Messages.Remove(toDelete);

            DbContext.SaveChanges();

            return toDelete;
        }

        public Message Find(int id)
        {
            return DbContext.Messages
                .Include(message => message.Recipient)
                .Include(message => message.Sender)
                .SingleOrDefault(message => message.Id == id);
        }

        public List<Message> FetchAll()
        {
            var messageTask = DbContext.Messages
                .Include(message => message.Sender)
                .Include(message => message.Recipient)
                .ToListAsync();
            messageTask.Wait();

            return messageTask.Result;
        }
    }
}