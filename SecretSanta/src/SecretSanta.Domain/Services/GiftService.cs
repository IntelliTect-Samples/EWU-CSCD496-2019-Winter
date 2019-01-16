using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class GiftService
    {
        /*
        public Gift(string title, int importance, string url, string description, User user)
        {
            if (title != null && url != null && description != null && user != null)
            {
                Title = title;
                Importance = importance;
                URL = url;
                Description = description;
                User = user;
            }
            else
            {
                throw new Exception("Gift constructor passed parameter was null");
            }
        }

        public bool EditGift(string title, int importance, string url, string description, User user)
        {
            if (title != null && url != null && description != null && user != null)
            {
                Title = title;
                Importance = importance;
                URL = url;
                Description = description;
                User = user;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteGift(User user)
        {
            if (user != null)
            {
                if (user.Gifts.Contains(this))
                {
                    user.Gifts.Remove(this);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        */
    }
}
