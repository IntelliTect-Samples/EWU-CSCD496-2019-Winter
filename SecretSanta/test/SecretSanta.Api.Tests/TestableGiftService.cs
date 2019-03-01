using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> ToReturn { get; set; }
        public int GetGiftsForUser_UserId { get; set; }

        public Task<Gift> AddGiftToUser(int userId, Gift gift)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> GetGift(int giftId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Gift>> GetGiftsForUser(int userId)
        {
            await Task.Yield();
            GetGiftsForUser_UserId = userId;
            return await Task.FromResult(ToReturn);
        }

        public Task<User> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGift(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> UpdateGiftForUser(int userId, Gift gift)
        {
            throw new NotImplementedException();
        }
    }
}
