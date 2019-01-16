using src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace src.Services
{
    class GiftsService
    {
        private ApplicationDbContext _db { get; }

        public GiftsService(ApplicationDbContext db)
        {
            _db = db;
        }

        private User FindUser(int usersId)
        {
            return _db.Users.Find(usersId);
        }

        public Gift FindGift(int giftId)
        {
            return _db.Gifts.Find(giftId);
        }

        public void AddGiftToDb(Gift gift)
        {
            Gift possibleGift = FindGift(gift.Id);
            if (IsGiftNull(possibleGift))
            {
                _db.Gifts.Add(possibleGift);
                _db.SaveChangesAsync().Wait();
            }
        }

        public void AddGiftToUser(int userId, Gift gift)
        {
            //Group foundGroup = FindGroup(id);
            /*if (user.FirstName == null || user.LastName == null)
            {
                //do not add;
            }
            else*/
            {
                User foundUser = _db.Users.Find(userId);
                foundUser.GiftList.Add(gift);
                //_db.Gifts.Add(gift);
                var saveChanges = _db.SaveChangesAsync();
                saveChanges.Wait();
            }
        }

        public void EditGiftOfUser(int userId, Gift gift)
        {
            User foundUser = _db.Users.Find(userId);
            foundUser.GiftList.Remove(gift);
            var saveChanges = _db.SaveChangesAsync();
            saveChanges.Wait();
        }

        public void RemoveGiftOfUser(int userId, Gift gift)
        {
            User foundUser = _db.Users.Find(userId);
            foundUser.GiftList.Remove(gift);
            var saveChanges = _db.SaveChangesAsync();
            saveChanges.Wait();
        }

        private bool IsUserNull(User user)
        {
            return user == null;
        }

        private bool IsGiftNull(Gift group)
        {
            return group == null;
        }


    }
}
