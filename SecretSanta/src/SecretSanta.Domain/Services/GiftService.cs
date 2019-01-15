using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private SecretSantaDbContext DbContext { get; }

        public GiftService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        // TODO: Create, Edit, Delete gifts for a given User.
        public void CreateGift(User user, Gift gift)
        {
            DbContext.Gifts.Add(gift);

            if(!user.ListOfGifts.Contains(gift))
            {
                user.ListOfGifts.Add(gift);
            }

            DbContext.SaveChanges();
        }

        public void EditGift(User user, Gift gift)
        {
            if (gift.ID != default(int))
                DbContext.Gifts.Update(gift);
            else
                DbContext.Gifts.Add(gift);

            List<Gift> set = (List<Gift>)user.ListOfGifts;

            for(int index = 0; index < set.Count; index++)
            {
                if(set[index].Title == gift.Title)
                {
                    set[index] = gift;
                    break;
                }
            }

            user.ListOfGifts = set;
            DbContext.SaveChanges();
        }

        public void DeleteGift(User user, Gift gift)
        {
            DbContext.Gifts.Remove(gift);

            user.ListOfGifts.Remove(gift);

            DbContext.SaveChanges();
        }
    }
}
