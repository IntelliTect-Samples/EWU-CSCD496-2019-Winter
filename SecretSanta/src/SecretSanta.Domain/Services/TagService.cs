using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class TagService
    {
        private ApplicationDbContext DbContext { get; }
        public TagService(ApplicationDbContext context)
        {
            DbContext = context;
        }
    }
}
