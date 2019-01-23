using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.UserGiftImport
{
    public class UserGiftImporter : IDisposable
    {
        public StreamReader StreamReader { get; set; }

        public void Open(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException();
            }
            else if (!File.Exists(fileName))
            {
                throw new ArgumentException("File does not exist.");
            }
            else if (File.Exists(fileName))
            {
                StreamReader = new StreamReader(fileName);
            }
        }

        public void Close()
        {
            if (StreamReader != null)
            {
                StreamReader.Close();
                StreamReader = null;
            }
        }

        public string ReadNext()
        {
            return StreamReader.ReadLine();
        }

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
            }
        }

        public string[] ExtractHeader(string header)
        {
            if (header == null)
            {
                throw new ArgumentNullException();
            }

            else
            {
                header = header.Trim();
                string headerType = header.Substring(0, header.IndexOf(":"));

                header = header.Substring(header.IndexOf(":") + 1);
                header = header.Trim();

                if (header.Contains(","))
                {
                    string lastName = header.Substring(0, header.IndexOf(","));
                    string firstName = header.Substring(header.IndexOf(",") + 2);

                    return new string[] { headerType, firstName, lastName };
                }
                else
                {
                    string firstName = header.Substring(0, header.IndexOf(" "));
                    string lastName = header.Substring(header.IndexOf(" ") + 1);

                    return new string[] { headerType, firstName, lastName };
                }
            }
        }

        public List<string> ReadGifts()
        {
            List<string> giftNames = new List<string>();

            while (!StreamReader.EndOfStream)
            {
                string line = ReadNext();
                if (line != "")
                {
                    giftNames.Add(line);
                }
            }

            return giftNames;
        }

        public User PopulateUser(string fileName)
        {
            Open(fileName);

            string[] header = ExtractHeader(ReadNext());
            string firstName = header[1];
            string lastName = header[2];

            List<string> giftNames = ReadGifts();
            List<Gift> gifts = new List<Gift>();

            foreach (string giftName in giftNames)
            {
                gifts.Add(new Gift { Title = giftName });
            }

            return new User { FirstName = firstName, LastName = lastName, Gifts = gifts };
        }

        public void Import(string fileName)
        {
            User user = PopulateUser(fileName);

            // FIXME: Add the user to the database
            // UserService userService = new UserService(new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>()));
            // userService.AddUser(user);
        }
    }
}
