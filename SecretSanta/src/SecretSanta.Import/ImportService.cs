using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.Import
{
    public class ImportService : IDisposable
    {

        public GiftService GiftService { get; set; }
        public UserService UserService { get; set; }
        public static int InstanceCount { get; set; }
        public StreamReader Istream { get; private set; }

        public ImportService(string path, SecretSantaDbContext context)
        {
            if (File.Exists(path))
            {
                Istream = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                InstanceCount++;
                GiftService = new GiftService(context);
                UserService = new UserService(context);
            }
            else
            {
                throw new FileNotFoundException("File does not exist error");
            }
        }

        ~ImportService()
        {
            Dispose();
        }

        public string ReadLine()
        {
            return Istream.ReadLine();
        }

        public List<Gift> BuildGiftList()
        {
            List<Gift> list = new List<Gift>();

            while(!Istream.EndOfStream)
            {
                string temp = Istream.ReadLine();

                if(temp.Length != 0)
                    list.Add(new Gift() { Title = temp });
            }

            return list;
        }

        public string[] ParseName(string word)
        {
            bool hasComma = false;
            string[] name = new string[2];
            string[] temp;

            if (word.Contains(":"))
            {
                temp = word.Split(':');
            }
            else
            {
                throw new FormatException("The line passed in (" + word + ") does not meet the define standard.");
            }

            if(temp.Length == 2 && temp[0].Length != 0 && temp[1].Length != 0)
            {
                temp[1] = temp[1].Trim();

                if (temp[1].Contains(","))
                {
                    temp = temp[1].Split(",");
                    hasComma = true;
                }
                else if(temp[1].Contains(" "))
                {
                    temp = temp[1].Split(" ");
                }
                else
                {
                    throw new FormatException("Missing first or last name.");
                }
            }
            else
            {
                throw new FormatException("Missing name after the :");
            }

            if(temp[0].Length == 0 || temp[1].Length == 0)
            {
                throw new FormatException("Missing first or last name.");
            }

            temp[0] = temp[0].Trim();
            temp[1] = temp[1].Trim();

            if (hasComma)
            {
                name[0] = temp[1];
                name[1] = temp[0];
            }
            else
            {
                name = temp;
            }

            return name;
        }

        public void PlaceUser(User user)
        {
            UserService.UpsertUser(user);
        }

        public User BuildUser(string[] name, string[] giftList)
        {
            User user = new User();

            if (name.Length == 2)
            {
                user.First = name[0];
                user.Last = name[1];

                foreach(string s in giftList)
                {
                    user.Gifts.Add(new Gift() { Title = s });
                }
            }

            return user;
        }

        public User BuildUser(string[] name, List<Gift> list)
        {
            User user = new User();

            if(name.Length == 2)
            {
                user.First = name[0];
                user.Last = name[1];

                foreach(Gift g in list)
                {
                    user.Gifts.Add(g);
                }
            }

            return user;
        }

        public void Dispose()
        {
            Istream?.Close();
            Istream?.Dispose();

            Istream = null;

            InstanceCount--;
            System.GC.SuppressFinalize(this);
        }
    }
}
