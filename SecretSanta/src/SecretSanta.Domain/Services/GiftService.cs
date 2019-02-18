using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Gift> AddGiftToUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Add(gift);
            await DbContext.SaveChangesAsync();

            return gift;
        }

        public async Task<Gift> UpdateGiftForUser(int userId, Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            gift.UserId = userId;
            DbContext.Gifts.Update(gift);
            await DbContext.SaveChangesAsync();

            return gift;
        }

        public async Task<List<Gift>> GetGiftsForUser(int userId)
        {
            return await DbContext.Gifts.Where(g => g.UserId == userId).ToListAsync();
        }

        public async Task RemoveGift(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            DbContext.Gifts.Remove(gift);
            await DbContext.SaveChangesAsync();
        }
    }
}