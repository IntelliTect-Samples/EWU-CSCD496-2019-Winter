using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> GetGiftsForUser_Return { get; set; }
        public int GetGiftsForUser_UserId { get; set; }

        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return GetGiftsForUser_Return;
        }


        public Gift AddGiftToUser_Return { get; set; }

        public int AddGiftToUser_UserId { get; set; }
        public Gift AddGiftToUser_Gift { get; set; }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            AddGiftToUser_UserId = userId;
            AddGiftToUser_Gift = gift;

            return AddGiftToUser_Return;
        }

        public Gift UpdateGiftForUser_Return { get; set; }
        public int UpdateGiftForUser_userId { get; set; }
        public Gift UpdateGiftForUser_Gift { get; set; }
        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            UpdateGiftForUser_userId = userId;
            UpdateGiftForUser_Gift = gift;

            return UpdateGiftForUser_Return;
        }

        public Gift RemoveGift_Gift { get; set; }
        public void RemoveGift(Gift gift)
        {
            RemoveGift_Gift = gift;
        }
    }
}