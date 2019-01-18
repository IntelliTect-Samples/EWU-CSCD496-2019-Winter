using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    class GiftServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public GiftServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        //TODO
        public Gift AddGift(Gift gift)
        {
            DbContext.Gift.Add(gift);
            DbContext.SaveChanges();
            return gift;
        }

        public Gift UpdateGift(Gift gift)
        {
            DbContext.Gift.Update(gift);
            DbContext.SaveChanges();
            return gift;
        }

        public Gift RemoveGift(Gift gift)
        {
            DbContext.Gift.Remove(gift);
            DbContext.SaveChanges();
            return gift;
        }
    }
}
