using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SecretSanta.Domain.Models;

namespace SecretSanta.Import
{
    public static class ImportUtility
    {
        private const string ProperFormatException =
            "Format must be either \"Name: <Firstname> <Lastname>\" or \"Name: <Lastname>, <Firstname>\"";
        
        public static User Import(string fileName)
        {
            if(String.IsNullOrEmpty(fileName)) { throw new NullReferenceException(); }
            
            string headerLine = GetHeader(fileName);
            
            Console.WriteLine(headerLine);

            if (!headerLine.StartsWith("Name:"))
            {
                throw new ArgumentException("Header must start with \"Name:\"", headerLine);
            }
            
            headerLine = headerLine.Replace("Name: ", "")
                .Trim().Replace(@"\s+", " ");
            
            List<string> splitName;
            
            if (headerLine.Contains(','))
            {
                splitName = headerLine.Split(',').Reverse().ToList();
            }
            else
            {
                splitName = headerLine.Split().ToList();
            }

            if (splitName.Count != 2)
            {
                throw new ArgumentException(ProperFormatException, headerLine);
            }

            string firstName = splitName[0].Trim();
            string lastName = splitName[1].Trim();

            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException(ProperFormatException, headerLine);
            }
            
            return new User()
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        private static string GetHeader(string fileName)
        {
            return File.ReadLines(fileName).First();
        }
    }
}