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

        public void AddUser(User user)
        {
            if (IsFullNameNull(user) || IsUserNull(user))
            {
                //do not add;
            }
            else
            {
                Db.Users.Add(user);
                Db.SaveChangesAsync().Wait();
            }
        }

        public void UpdateUser(User user)
        {
            if (IsFullNameNull(user) || IsUserNull(user))
            {
                //do not add;
            }
            else
            {
                Db.Users.Update(user);
                Db.SaveChangesAsync().Wait();
            }
        }

        public User FindUser(int id)
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
