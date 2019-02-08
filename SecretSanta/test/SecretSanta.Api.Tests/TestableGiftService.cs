using System.Collections.Generic;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public int AddGiftToUser_UserId { get; set; }

        public List<Gift> ToReturn { get; set; }
        public Gift Return { get; set; }
        public bool RemoveReturn { get; set; }
        public int RemoveGift_GiftId { get; set; }
        public int GetGiftsForUser_UserId { get; set; }
        public int UpdateGift_UserId { get; set; }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            AddGiftToUser_UserId = userId;
            return Return;
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return ToReturn;
        }

        public bool RemoveGift(int giftId)
        {
            RemoveGift_GiftId = giftId;
            return RemoveReturn;
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            UpdateGift_UserId = userId;
            return Return;
        }
    }
}