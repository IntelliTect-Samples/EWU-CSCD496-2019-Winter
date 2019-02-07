using System.Collections.Generic;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> ToReturn { get; set; }
        public int GetGiftsForUser_UserId { get; set; }

        public Gift AddGiftToUser(int userId, Gift gift)
        {
            throw new System.NotImplementedException();
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return ToReturn;
        }

        public void RemoveGift(Gift gift)
        {
            throw new System.NotImplementedException();
        }

        public Gift UpdateGiftForUser(int userId, Gift gift)
        {
            throw new System.NotImplementedException();
        }
    }
}