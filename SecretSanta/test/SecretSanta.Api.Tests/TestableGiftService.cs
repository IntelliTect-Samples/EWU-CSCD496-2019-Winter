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
            return true;
        }

        public bool CreateGift(int uid, Gift gift)
        {
            return true;
        }

        public bool DeleteGift(User user, Gift gift)
        {
            return true;
        }

        public bool DeleteGift(int uid, int gid)
        {
            return true;
        }

        public bool EditGift(User user, Gift gift)
        {
            return true;
        }

        public bool EditGift(int uid, Gift gift)
        {
            return true;
        }

        public User FindUser(int id)
        {
            return new User() { Id = id };
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            GetGiftsForUser_UserId = userId;
            return ToReturn;
        }
    }
}