using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        Gift AddGiftToUser(int giftId, Gift gift);
        List<Gift> GetGiftsForUser(int giftId);
        void RemoveGift(int giftId, Gift gift);
        Gift UpdateGiftForUser(int giftId, Gift gift);
    }
}