using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GroupServices
    {
        private ApplicationDbContext DbContext { get; set; }
        
        public GroupServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
