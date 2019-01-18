using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class MessageService : ApplicationService
    {
        public MessageService(ApplicationDbContext dbContext) : base(dbContext) { }

        public Message AddMessage(Message message)
        {
            DbContext.Messages.Add(message);
            DbContext.SaveChanges();

            return message;
        }

        public List<Message> GetConversation(Pairing pairing)
        {
            return new List<Message>();     //TODO
        }
    }
}