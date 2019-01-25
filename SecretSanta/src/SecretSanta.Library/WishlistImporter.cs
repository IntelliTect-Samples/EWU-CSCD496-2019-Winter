using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using SecretSanta.Domain;

namespace SecretSanta.Library
{
    public class WishlistImporter
    {
        public string Filename { get; }

        public WishlistImporter(string fname)
        {
            if (String.IsNullOrEmpty(fname))
                throw new ArgumentException("Filename cannot be null or empty.");
            if (!File.Exists(fname))
                throw new ArgumentException("Specified file does not exist: " + fname);
            if (!File.ReadLines(fname).Any())
                throw new ArgumentException("File structure is invalid. " + fname);
            Filename = fname;
        }

        public User GetUser()
        {
            string[] name = File.ReadLines(Filename).First().Split(':');
            if (name[0] != "Name")
                throw new ArgumentException("Name structure invalid1");

            name = name[1].Split(',');

            if (name.Length == 1)
            {
                name = name[0].Trim().Split(null);
                if (name.Length == 2)
                    return new User { FirstName = name[0].Trim(), LastName = name[1].Trim() };
                else
                    throw new ArgumentException("Name structure invalid2");
            }
            if (name.Length == 2)
                return new User { FirstName = name[1].Trim(), LastName = name[0].Trim() };

            throw new ArgumentException("Name structure invalid3");
        }

        public List<Gift> SetGifts(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            List<String> giftNames = File.ReadAllLines(Filename).ToList<String>();
            giftNames.RemoveAt(0);
            List<Gift> gifts = new List<Gift>();
            foreach (string name in giftNames)
            {
                if (!String.IsNullOrEmpty(name) )
                    gifts.Add(new Gift { Title = name });
            }
                
            user.UserGifts = gifts;
            return gifts;
        }

    }
}
