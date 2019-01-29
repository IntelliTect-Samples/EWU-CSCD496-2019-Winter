using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        User FindUser(int id);
        bool CreateGift(User user, Gift gift);
        bool CreateGift(int uid, string giftTitle);
        bool EditGift(User user, Gift gift);
        bool DeleteGift(User user, Gift gift);
        List<Gift> GetGiftsForUser(int userId);
    }
}