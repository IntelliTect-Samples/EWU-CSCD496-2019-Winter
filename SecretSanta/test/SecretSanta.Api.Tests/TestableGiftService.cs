using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> GetGiftsForUser_Return { get; set; }
        public int GetGiftsForUser_UserId { get; set; }

        public Gift AddGiftToUser_Return { get; set; }
        public int AddGiftToUser_UserId { get; set; }
        public Gift AddGiftToUser_Gift { get; set; }

        public Gift RemoveGiftToUser_Gift { get; set; }

        public Gift UpdateGiftToUser_Return { get; set; }
        public int UpdateGiftToUser_UserId { get; set; }
        public Gift UpdateGiftToUser_Gift { get; set; }

        Gift IGiftService.AddGiftToUser(int userId, Gift gift)
        {
            AddGiftToUser_UserId = userId;
            AddGiftToUser_Gift = gift;

            return AddGiftToUser_Return;
        }

        List<Gift> IGiftService.GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return GetGiftsForUser_Return;
        }

        void IGiftService.RemoveGift(Gift gift)
        {
            RemoveGiftToUser_Gift = gift;
        }

        Gift IGiftService.UpdateGiftForUser(int userId, Gift gift)
        {
            UpdateGiftToUser_UserId = userId;
            UpdateGiftToUser_Gift = gift;

            return UpdateGiftToUser_Return;
        }
    }
}