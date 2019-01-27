using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private ApplicationDbContext DbContext { get; set; }
        public GiftService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Gift UpsertGift(Gift gift)
        {
            if (gift.Id == 0)
            {
                DbContext.Gifts.Add(gift);
            }
            else
            {
                DbContext.Gifts.Update(gift);
            }
            DbContext.SaveChanges();
            return gift;
        }

        public Gift DeleteGift(Gift gift)
        {
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
        public void RemoveGift(Gift gift)
            if (gift == null) throw new ArgumentNullException(nameof(gift));
        {

            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();
        }
    }
}