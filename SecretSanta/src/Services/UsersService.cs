using src.Models;
using System;
using System.Collections.Generic;
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

        //not required
        /*public void RemoveUser(User user)
        {
            if (user.FirstName == null || user.LastName == null)
            {
                //do not add;
            }
            else
            {
                Db.Users.Add(user);

                Db.Remove.
                var saveChanges = Db.SaveChangesAsync();
                saveChanges.Wait();
            }
        }*/

        public void UpdateUser(User user)
        {
            if (IsNameNotNull(user) || IsUserNull(user))
            {
                //do not add;
            }
            else
            {
                User actualUser = Db.Users.Find(user);

                if (IsUserNull(actualUser))
                {
                    //do nothing
                    return;
                }
                /*if (IsFirstNameNotNull(user))
                {
                    actualUser.FirstName = user.FirstName;
                }
                if (IsLastNameNotNull(user))
                {
                    actualUser.LastName = user.LastName;
                }
                if (IsGiftListNull(user))
                {
                    actualUser.GiftList = user.GiftList;
                }
                if (IsGroupListNull(user))
                {
                    actualUser.GroupList = user.GroupList;
                }
                */
                Db.Users.Update(user);
                var saveChanges = Db.SaveChangesAsync();
                saveChanges.Wait();
            }
        }

        private bool IsFirstNameNotNull(User user)
        {
            if (user.FirstName == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsLastNameNotNull(User user)
        {
            if (user.LastName == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsNameNotNull(User user)
        {
            if (IsFirstNameNotNull(user) || IsLastNameNotNull(user))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsGiftListNull(User user)
        {
            return user.GiftList == null;
        }

        private bool IsGroupListNull(User user)
        {
            return user.GroupList == null;
        }

        private bool IsUserNull(User user)
        {
            return user == null;
        }
    }
}
