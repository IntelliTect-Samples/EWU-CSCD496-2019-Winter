using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> GiftsToReturn { get; set; }
        public int LastModifiedUserId { get; set; }
        public Gift LastModifiedGift { get; set; }

        public List<Gift> GetGiftsForUser(int userId)
        {
            LastModifiedUserId = userId;
            return GiftsToReturn;
        }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            LastModifiedUserId = userId;
            LastModifiedGift = gift;
            return gift;
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            LastModifiedUserId = userId;
            LastModifiedGift = gift;
            return gift;
        }

        public void RemoveGift(Gift gift)
        {
            LastModifiedGift = gift;
        }
    }
}