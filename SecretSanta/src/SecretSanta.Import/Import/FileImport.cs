using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SecretSanta.Import.Import
{
    public class FileImport
    {
        public static User ReadHeaderFromFile(string fileName)
        {
            if(fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            string filePath, fileLine;
            StreamReader streamReader;

            try
            {
                filePath = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, fileName);
                streamReader = new StreamReader(filePath);
                fileLine = streamReader.ReadLine();
            } 
            catch (ArgumentException e)
            {
                throw new ArgumentException($"{e}");
            }

            return ExtractUser(fileLine);   
        }

        public static User ExtractUser(string str)
        {
            string[] userNames;
            User user;
            str = str.Trim();

            if (str == String.Empty)
            {
                throw new ArgumentException("Passed in string was empty.");
            }
            else if (str.Contains(','))
            {
                userNames = str.Split(',');
                user = new User
                {
                    FirstName = userNames[1].Trim(),
                    LastName = userNames[0]
                };
            }
            else
            {
                userNames = str.Split(' ');
                user = new User
                {
                    FirstName = userNames[0],
                    LastName = userNames[1]
                };
            }
            return user;
        }
    }
}
