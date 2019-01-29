using System.Collections.Generic;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Interfaces;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
        public List<Gift> ToReturn { get; set; }
        public int GetGiftsForUser_UserId { get; set; }

        public bool CreateGift(User user, Gift gift)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteGift(User user, Gift gift)
        {
            throw new System.NotImplementedException();
        }

        public bool EditGift(User user, Gift gift)
        {
            throw new System.NotImplementedException();
        }

        public User FindUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return ToReturn;
        }
    }
}