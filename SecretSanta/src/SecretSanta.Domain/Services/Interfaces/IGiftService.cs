using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IGiftService
    {
        Task<Gift> GetGift(int giftId);
        Task<Gift> AddGift(Gift gift);
        Task<Gift> UpdateGift(Gift gift);
        Task RemoveGift(int giftId);
        Task<List<Gift>> GetGiftsForUser(int userId);
    }
}
