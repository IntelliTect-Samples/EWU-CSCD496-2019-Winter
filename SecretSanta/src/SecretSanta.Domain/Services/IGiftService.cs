using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public interface IGiftService
    {
        List<Gift> GetGiftsForUser(int userId);
        Gift AddGiftToUser(Gift gift, int userId);
        Gift DeleteGiftFromUser(Gift gift, int userId);
        Gift UpdateGiftForUser(Gift gift, int userId);
    }
}