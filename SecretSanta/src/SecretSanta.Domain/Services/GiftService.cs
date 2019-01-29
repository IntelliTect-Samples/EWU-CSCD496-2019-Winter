using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class GiftService : IGiftService
    {
        private SecretSantaDbContext DbContext { get; set; }

        public GiftService(SecretSantaDbContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(DbContext));
        }

        public Gift AddGift(Gift gift)
        {
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGift(Gift gift)
        {
            DbContext.Gifts.Update(gift);
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

        public List<Gift> GetGiftsForUser(int userId)
        {
            return DbContext.Gifts.Where(g => g.Id == userId).ToList();
        }
    }
}
