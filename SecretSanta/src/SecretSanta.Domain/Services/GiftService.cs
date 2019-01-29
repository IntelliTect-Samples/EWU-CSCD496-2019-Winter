using SecretSanta.Domain.Models;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class GiftService : IGiftService
    {
        private SecretSantaDbContext DbContext { get; }

        public GiftService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public bool CreateGift(User user, Gift gift)
        {
            bool test = false;

            DbContext.Gifts.Add(gift);

            if(!user.Gifts.Contains(gift))
            {
                user.Gifts.Add(gift);
                test = true;
            }

            DbContext.SaveChanges();
            return test;
        }

        public bool EditGift(User user, Gift gift)
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

            return true;
        }

        public bool DeleteGift(User user, Gift gift)
        {
            DbContext.Gifts.Remove(gift);

            user.Gifts.Remove(gift);

            DbContext.SaveChanges();

            if (DbContext.Gifts.Find(gift.Id) == null)
            {
                return true;
            }

            return false;
        }

        public bool HasGift(User user, Gift gift)
        {
            return user.Gifts.Contains(gift);
        }

        public Gift FindGift(User user, int id)
        {
            List<Gift> giftList = (List<Gift>)user.Gifts;
            Gift gift = null;

            if(id > 0)
            {
                foreach(Gift g in giftList)
                {
                    if (g.Id == id)
                        gift = g;
                }
            }

            return gift;
        }

        public List<Gift> GetGiftsForUser(int userId)
        {
            return (List<Gift>)DbContext.Users.Find(userId).Gifts;
        }

        public User FindUser(int id)
        {
            return DbContext.Users.Find(id);
        }

        public bool CreateGift(int uid, string giftTitle)
        {
            bool test = false;

            User user = DbContext.Users.Find(uid);
            Gift gift = new Gift() { Title = giftTitle };

            test = CreateGift(user, gift);

            return test;
        }
    }
}
