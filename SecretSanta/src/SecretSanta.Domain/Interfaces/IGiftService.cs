using System.Collections.Generic;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Interfaces
{
    public interface IGiftService
    {
        /// <summary>
        /// Adds a gift to the database
        /// </summary>
        /// <param name="gift"></param>
        /// <returns></returns>
        Gift AddGift(Gift gift);

        /// <summary>
        /// Updates an existing gift in the database
        /// </summary>
        /// <param name="gift"></param>
        /// <returns></returns>
        Gift UpdateGift(Gift gift);

        /// <summary>
        /// Deletes a gift in the database
        /// </summary>
        /// <param name="gift"></param>
        /// <returns></returns>
        Gift DeleteGift(Gift gift);

        /// <summary>
        /// Searches for the gift with specified id and returns it if found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Gift Find(int id);

        /// <summary>
        /// Gets the gifts for the user with specified user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Gift> GetGiftsForUser(int userId);
    }
}
