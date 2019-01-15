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

        public void AddGift(int userId, Gift gift)
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
                _db.Gifts.Add(gift);
                var saveChanges = _db.SaveChangesAsync();
                saveChanges.Wait();
            }
        }

        public void EditGift(int userId, Gift gift)
        {
            User foundUser = _db.Users.Find(userId);
            foundUser.GiftList.Remove(gift);
            var saveChanges = _db.SaveChangesAsync();
            saveChanges.Wait();
        }

        public void RemoveGift(int userId, Gift gift)
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

        private bool IsGroupNull(Group group)
        {
            return group == null;
        }


    }
}
