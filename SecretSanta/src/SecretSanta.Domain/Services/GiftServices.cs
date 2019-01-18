using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftServices
    {
        private ApplicationDbContext DbContext { get; set; }

        public GiftServices(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        //TODO
        public Gift AddUpdateGift(Gift gift)
        {
            if (gift.Id == default(int))
                DbContext.Gift.Add(gift);
            else
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
