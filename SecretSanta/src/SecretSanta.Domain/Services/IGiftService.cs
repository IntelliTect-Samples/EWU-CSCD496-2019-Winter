using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        List<Gift> GetGiftsForUser(int userId);
        Gift AddGiftToUser(int userId, Gift gift);
    }
}