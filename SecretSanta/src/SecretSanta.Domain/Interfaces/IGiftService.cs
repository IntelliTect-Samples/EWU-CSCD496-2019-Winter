using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        void CreateGift(User user, Gift gift);
        void EditGift(User user, Gift gift);
        void DeleteGift(User user, Gift gift);
        List<Gift> GetGiftsForUser(int userId);
    }
}