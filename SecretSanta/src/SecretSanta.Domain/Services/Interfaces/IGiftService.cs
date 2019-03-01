using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IGiftService
    {
        Task<List<Gift>> GetGiftsForUser(int userId);
        Task<Gift> AddGiftToUser(int userId, Gift gift);
        Task RemoveGift(int userId, int giftId);
        Task<Gift> UpdateGiftForUser(int userId, Gift gift);
        Task<User> GetUser(int userId);
    }
}
