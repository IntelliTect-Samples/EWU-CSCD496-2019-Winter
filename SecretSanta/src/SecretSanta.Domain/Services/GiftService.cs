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

        public async Task<Gift> AddGift(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            DbContext.Gifts.Add(gift);
            await DbContext.SaveChangesAsync();

            return gift;
        }

        public async Task<Gift> GetGift(int giftId)
        {
            var retrievedGift = await DbContext.Gifts.SingleOrDefaultAsync(g => g.Id == giftId);

            return retrievedGift;
        }

        public async Task<Gift> UpdateGift(Gift gift)
        {
            if (gift == null) throw new ArgumentNullException(nameof(gift));

            DbContext.Gifts.Update(gift);
            await DbContext.SaveChangesAsync();

            return gift;
        }

        public async Task<List<Gift>> GetGiftsForUser(int userId)
        {
            return await DbContext.Gifts.Where(g => g.UserId == userId).ToListAsync();
        }

        public async Task RemoveGift(int giftId)
        {
            var giftToDelete = await DbContext.Gifts.FindAsync(giftId);

            if (giftToDelete != null)
            {
                DbContext.Gifts.Remove(giftToDelete);
                DbContext.SaveChanges();
            }
        }
    }
}