using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private SecretSantaDbContext DbContext { get; }

        public GiftService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        // TODO: Create, Edit, Delete gifts for a given User.
    }
}
