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

        public async Task<Gift> GetGift(int giftId)
        {
            return await DbContext.Gifts.FindAsync(giftId);
        }

        public async Task<User> GetUser(int userId)
        {
            return await DbContext.Users.FindAsync(userId);
        }

        public async Task RemoveGift(int userId, int giftId)
        {
            Gift gift = await DbContext.Gifts.FindAsync(giftId);
            User user = await DbContext.Users.FindAsync(userId);

            DbContext.Gifts.Remove(gift);
            user.Gifts.Remove(gift);
            await DbContext.SaveChangesAsync();
        }
    }
}