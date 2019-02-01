using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        Gift AddGiftToUser(int userId, Gift gift);

        Gift UpdateGiftForUser(int userId, Gift gift);

        List<Gift> GetGiftsForUser(int userId);

        Gift RemoveGift(Gift gift);
    }
}