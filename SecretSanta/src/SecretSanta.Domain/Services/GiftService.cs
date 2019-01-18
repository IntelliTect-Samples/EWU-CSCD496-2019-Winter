using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        public GiftService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        private ApplicationDbContext DbContext { get; }

        public Gift UpsertGift(Gift gift)
        {
            if (gift.Id == default(int))
                DbContext.Gifts.Add(gift);
            else
                DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift DeleteGift(Gift toDelete)
        {
            DbContext.Gifts.Remove(toDelete);

            DbContext.SaveChanges();

            return toDelete;
        }

        public Gift Find(int id)
        {
            return DbContext.Gifts
                .Include(g => g.User)
                .SingleOrDefault(g => g.Id == id);
        }

        public List<Gift> FetchAll()
        {
            var giftTask = DbContext.Gifts.ToListAsync();
            giftTask.Wait();

            return giftTask.Result;
        }
    }
}