using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;

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
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift));
            }
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift));
            }
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            gift.UserId = userId;
            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            return DbContext.Gifts.Where(g => g.UserId == userId).ToList();
        }

        public void RemoveGift(int userId, Gift gift)
        {
            if (gift == null)
            {
                throw new ArgumentNullException(nameof(gift));
            }
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            gift.UserId = userId;
            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();
        }
    }
}