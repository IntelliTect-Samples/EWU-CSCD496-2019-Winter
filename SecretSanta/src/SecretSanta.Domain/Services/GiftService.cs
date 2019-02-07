using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Services
{
    public class GiftService : IGiftService
    {
        private ApplicationDbContext DbContext { get; }

        public GiftService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            return DbContext.Gifts.Where(g => g.UserId == userId).ToList();
        }

        public bool RemoveGift(int giftId)
        {
            var giftToDelete = DbContext.Gifts.SingleOrDefault(g => g.Id == giftId);

            if (giftToDelete == null)
            {
                throw new InvalidOperationException("GiftId does not exist");
            }

            DbContext.Gifts.Remove(giftToDelete);
            return (DbContext.SaveChanges() == 1);
        }
    }
}