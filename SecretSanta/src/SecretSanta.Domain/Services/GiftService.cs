using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService : IGiftService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GiftService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Gift CreateGift(Gift gift)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));
            if (gift.Id == 0)
            {
                DbContext.Gifts.Add(gift);
                DbContext.SaveChanges();
            }
            return gift;
        }

        public Gift UpdateGift(Gift gift)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));
            return UpdateGift(gift, gift.Id);
        }

        public Gift UpdateGift(Gift gift, int giftId)
        {
            if (giftId <= 0) throw new ArgumentOutOfRangeException(nameof(giftId));

            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();
            return gift;
        }

        public Gift DeleteGift(int giftId)
        {
            return DeleteGift(Find(giftId));
        }

        public Gift DeleteGift(Gift gift)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));

            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();
            return gift;
        }

        public Gift Find(int id)
        {
            return DbContext.Gifts
                .Include(g => g.User)
                .SingleOrDefault(g => g.Id == id);
        }

        public Gift AddGiftToUser(Gift gift, int userId)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);

            DbContext.SaveChanges();
            return gift;
        }

        public Gift UpdateGiftForUser(Gift gift, int userId)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            gift.UserId = userId;
            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift DeleteGiftFromUser(Gift gift, int userId)
        {
            if (gift is null) throw new ArgumentNullException(nameof(gift));
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));

            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();
            return gift;
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
            return DbContext.Gifts.Where(g => g.UserId == userId).ToList();
        }
    }
}