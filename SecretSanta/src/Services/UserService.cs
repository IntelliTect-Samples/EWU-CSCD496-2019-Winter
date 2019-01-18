using Microsoft.EntityFrameworkCore;
using src.Model;
using src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace src.Services
{
    public class UsersService
    {
        private ApplicationDbContext Db { get; }

        public UsersService(ApplicationDbContext db)
        {
            Db = db;
        }

        public bool Add(User user)
        {
            if (IsFullNameNull(user) || IsUserNull(user))
            {
                return false;
            }
            else
            {
                Db.Users.Add(user);
                Db.SaveChangesAsync().Wait();
                return true;
            }
        }

        public bool Update(User user)
        {
            if (IsFullNameNull(user) || IsUserNull(user))
            {
                return false;
            }
            else
            {
                Db.Users.Update(user);
                Db.SaveChangesAsync().Wait();
                return true;
            }
        }

        public User Find(int id)
        {
            return Db.Users
                .Include(u => u.GiftList)
                .SingleOrDefault(u => u.Id == id);
        }

        public bool IsFirstNameNotNull(User user)
        {
            return user.FirstName != null;
        }

        public bool IsLastNameNotNull(User user)
        {
            return user.LastName != null;
        }

        public bool IsFullNameNull(User user)
        {
            return !(IsFirstNameNotNull(user) || IsLastNameNotNull(user));
        }

        public bool IsGiftListNull(User user)
        {
            return user.GiftList == null;
        }


        public bool IsUserNull(User user)
        {
            return user == null;
        }
    }
}
