using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IGiftService
    {
        Gift AddGiftToUser(int userId, Gift gift);
        Gift UpdateGiftForUser(int userId, Gift gift);
        List<Gift> GetGiftsForUser(int userId);
        bool RemoveGift(int giftId);
    }
}
