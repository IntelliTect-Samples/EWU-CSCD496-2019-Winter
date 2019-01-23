using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SecretSanta.Import.Import
{
    public static class FileImportService
    {
        public static (User user, List<Gift> gifts) ImportFile(string fileName)
        {
            if(fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            User user;
            List<Gift> gifts = new List<Gift>();
            string filePath = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, fileName);

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    user = ExtractUser(sr.ReadLine());

                    string fileLine;
                    while((fileLine = sr.ReadLine()) != null)
                    {
                        Gift gift = ExtractGift(fileLine);
                        if(gift != null)
                        {
                            gifts.Add(ExtractGift(fileLine));
                        }
                    }
                    sr.Close();
                }
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"{e}");
            }
            return (user, gifts);
        }

        private static Gift ExtractGift(string fileLine)
        {
            if (string.IsNullOrWhiteSpace(fileLine)) return null;

            fileLine = fileLine.Trim();
            return new Gift { Title = fileLine };
        }

        private static User ExtractUser(string fileLine)
        {
            fileLine = ValidateHeader(fileLine);
            string[] userNames;
            User user;

            if (fileLine.Contains(','))
            {
                userNames = fileLine.Split(',');
                user = new User
                {
                    FirstName = userNames[1].Trim(),
                    LastName = userNames[0].Trim()
                };
            }
            else
            {
                userNames = fileLine.Split(' ');
                user = new User {
                    FirstName = userNames[0],
                    LastName = userNames[1]
                };
            }
            return user;
        }

        private static string ValidateHeader(string header)
        {
            if (string.IsNullOrWhiteSpace(header))
            {
                throw new ArgumentException("Passed in string was empty or null.");
            }
            if (!header.StartsWith("Name:", StringComparison.Ordinal))
            {
                throw new ArgumentException("Header must begin with \"Name: \"");
            }
            return header.Substring(header.IndexOf(':') + 1).Trim();
        }
    }
}
