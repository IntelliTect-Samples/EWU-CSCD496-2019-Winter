using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SecretSanta.Import.Import
{
    public class FileImport
    {
        public static void ReadHeaderFromFile(string fileName)
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

            User user = ExtractUser(fileLine);   
        }

        public static User ExtractUser(string str)
        {
            User user = new User();
            return user;
        }
    }
}
