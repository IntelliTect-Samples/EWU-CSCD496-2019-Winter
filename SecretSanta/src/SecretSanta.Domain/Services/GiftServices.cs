using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Services
{
    public class GiftService : ApplicationService
    {
        public GiftService(ApplicationDbContext dbContext) : base(dbContext) { }

        public Gift AddGift(Gift gift)
        {
            DbContext.Gifts.Add(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public Gift UpdateGift(Gift gift)
        {
            DbContext.Gifts.Update(gift);
            DbContext.SaveChanges();

            return gift;
        }

        public int RemoveGift(Gift gift)
        {
            int Id = gift.Id;
            DbContext.Gifts.Remove(gift);
            DbContext.SaveChanges();

            return Id;
        }

        public Gift FindById(int id)
        {
            return DbContext.Gifts.Find(id);
        } 
    }
}