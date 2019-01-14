using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class MessageService
    {
        private SecretSantaDbContext DbContext { get; }

        public MessageService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public void StoreMassage(Message message)
        {
            if(message.ID == default(int))
            {
                DbContext.Messages.Add(message);
            }
            else
            {
                DbContext.Messages.Update(message);
            }
            DbContext.SaveChanges();
        }
    }
}
