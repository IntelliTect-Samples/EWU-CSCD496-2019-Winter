using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public Gift CreateGift_Return { get; set; }
        public Gift CreateGift(Gift gift)
        {
            CreateGift_Return = gift;
            return CreateGift_Return;
        }

        public Gift UpdateGift_Return { get; set; }
        public Gift UpdateGift(Gift gift)
        {
            UpdateGift_Return = gift;
            return UpdateGift_Return;
        }

        public Gift DeleteGift_Return { get; set; }
        public Gift DeleteGift(Gift gift)
        {
            DeleteGift_Return = gift;
            return DeleteGift_Return;
        }

        public Gift AddGiftToUser_Gift { get; set; }
        public int AddGiftToUser_UserId { get; set; }
        public Gift AddGiftToUser(Gift gift, int userId)
        {
            AddGiftToUser_UserId = userId;
            AddGiftToUser_Gift = gift;

            return AddGiftToUser_Gift;
        }


        public Gift UpdateGiftForUser_Gift { get; set; }
        public int UpdateGiftForUser_UserId { get; set; }
        public Gift UpdateGiftForUser(Gift gift, int userId)
        {
            UpdateGiftForUser_Gift = gift;
            UpdateGiftForUser_UserId = userId;

            return UpdateGiftForUser_Gift;
        }

        public Gift DeleteGiftFromUser_Gift { get; set; }
        public int DeleteGiftFromUser_UserId { get; set; }
        public Gift DeleteGiftFromUser(Gift gift, int userId)
        {
            DeleteGiftFromUser_Gift = gift;
            DeleteGiftFromUser_UserId = userId;

            return DeleteGiftFromUser_Gift;
        }

        public List<Gift> GetGiftsForUser_Return { get; set; }
        public int GetGiftsForUser_UserId { get; set; }
        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return GetGiftsForUser_Return;
        }
    }
}