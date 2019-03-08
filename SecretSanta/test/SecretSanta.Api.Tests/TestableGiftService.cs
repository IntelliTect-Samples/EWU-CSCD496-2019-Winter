using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests
{
    public class TestableGiftService : IGiftService
    {
#pragma warning disable CS3003 // Type is not CLS-compliant
        public List<Gift> ToReturn { get; set; }
#pragma warning restore CS3003 // Type is not CLS-compliant
        public int GetGiftsForUser_UserId { get; set; }

#pragma warning disable CS3002 // Return type is not CLS-compliant
#pragma warning disable CS3001 // Argument type is not CLS-compliant
        public Task<Gift> AddGiftToUser(int userId, Gift gift)
#pragma warning restore CS3001 // Argument type is not CLS-compliant
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public Task<Gift> GetGift(int giftId)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public async Task<List<Gift>> GetGiftsForUser(int userId)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            await Task.Yield();
            GetGiftsForUser_UserId = userId;
            return await Task.FromResult(ToReturn).ConfigureAwait(false);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        public Task<User> GetUser(int userId)
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            throw new NotImplementedException();
        }

        public Task RemoveGift(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
#pragma warning disable CS3001 // Argument type is not CLS-compliant
        public Task<Gift> UpdateGiftForUser(int userId, Gift gift)
#pragma warning restore CS3001 // Argument type is not CLS-compliant
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            throw new NotImplementedException();
        }
    }
}
