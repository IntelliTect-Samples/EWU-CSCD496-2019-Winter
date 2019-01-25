﻿using Microsoft.EntityFrameworkCore;
using src.Model;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace src.Services
{
    public class GiftsService
    {
        private ApplicationDbContext Db { get; }

        public GiftsService(ApplicationDbContext db)
        {
            Db = db;
        }

        public Gift Find(int giftId)
        {
            return Db.Gifts
                .Include(g => g.User)
                .SingleOrDefault(g => g.Id == giftId);
        }

        public bool Add(Gift gift)
        {
            Gift possibleGift = Find(gift.Id);
            if (IsGiftNull(possibleGift))
            {
                Db.Gifts.Add(gift);
                Db.SaveChangesAsync().Wait();
                return true;
            }
            return false;
        }

        /*public void AddGiftToUser(int userId, Gift gift)
        {
            User foundUser = Db.Users.Find(userId);
            foundUser.GiftList.Add(gift);
            var saveChanges = Db.SaveChangesAsync();
            Db.SaveChangesAsync();
        }*/

        public bool Update(Gift gift)
        {
            Gift possibleGift = Find(gift.Id);
            if (!IsGiftNull(possibleGift))
            {
                Db.Gifts.Update(gift);
                Db.SaveChangesAsync().Wait();
                return true;
            }
            return false;
        }

        /*public void EditGiftOfUser(int userId, Gift gift)
        {
            User foundUser = Db.Users.Find(userId);
            foundUser.GiftList.Remove(gift);
            var saveChanges = Db.SaveChangesAsync();
            saveChanges.Wait();
        }*/

        /*public void RemoveGiftOfUser(int userId, Gift gift)
        {
            User foundUser = Db.Users.Find(userId);
            foundUser.GiftList.Remove(gift);
            var saveChanges = Db.SaveChangesAsync();
            saveChanges.Wait();
        }*/

        public Gift Delete(Gift possibleGift)
        {
            if (possibleGift != null)
            {
                Db.Gifts.Remove(possibleGift);
                Db.SaveChangesAsync().Wait();
            }
            return possibleGift;
        }

        /*
        public bool IsUserNull(User user)
        {
            return user == null;
        }*/

        public bool IsGiftNull(Gift group)
        {
            return group == null;
        }
    }
}
