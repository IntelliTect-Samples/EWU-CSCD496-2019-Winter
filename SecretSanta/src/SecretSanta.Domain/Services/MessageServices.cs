using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class MessageServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public MessageServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
