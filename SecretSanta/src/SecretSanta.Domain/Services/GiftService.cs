using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        private SecretSantaDbContext DbContext { get; }

        public GiftService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public void CreateGift(User user, Gift gift)
        {
            DbContext.Gifts.Add(gift);

            if(!user.Gifts.Contains(gift))
            {
                user.Gifts.Add(gift);
            }

            DbContext.SaveChanges();
        }

        public void EditGift(User user, Gift gift)
        {
            if (gift.Id != default(int))
                DbContext.Gifts.Update(gift);
            else
                DbContext.Gifts.Add(gift);

            List<Gift> set = (List<Gift>)user.Gifts;

            for(int index = 0; index < set.Count; index++)
            {
                if(set[index].Title == gift.Title)
                {
                    set[index] = gift;
                    break;
                }
            }

            user.Gifts = set;
            DbContext.SaveChanges();
        }

        public void DeleteGift(User user, Gift gift)
        {
            DbContext.Gifts.Remove(gift);

            user.Gifts.Remove(gift);

            DbContext.SaveChanges();
        }

        public bool HasGift(User user, Gift gift)
        {
            return user.Gifts.Contains(gift);
        }

        public Gift FindGift(User user, int id)
        {
            return null;
        }
    }
}
