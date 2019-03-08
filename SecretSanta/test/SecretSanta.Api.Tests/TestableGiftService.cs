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
        public int GetGiftsForUserUserId { get; set; }

        public Task<Gift> AddGift(Gift gift)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> GetGift(int giftId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gift>> GetGiftsForUser(int userId)
        {
            GetGiftsForUserUserId = userId;
            return Task.FromResult(ToReturn);
        }

        public Task RemoveGift(int giftId)
        {
            throw new NotImplementedException();
        }

        public Task<Gift> UpdateGift(Gift gift)
        {
            throw new NotImplementedException();
        }
    }
}
