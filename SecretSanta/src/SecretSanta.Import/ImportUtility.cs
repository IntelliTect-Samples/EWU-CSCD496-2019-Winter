using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SecretSanta.Domain.Models;

namespace SecretSanta.Import
{
    public static class ImportUtility
    {
        private const string ProperFormatException =
            "Format must be either \"Name: <Firstname> <Lastname>\" or \"Name: <Lastname>, <Firstname>\"";

        public static User Import(string fileName)
        {
            var properlyQualifiedFilename = GetProperlyQualifiedFilename(fileName);

            var headerLine = GetHeader(properlyQualifiedFilename);

            headerLine = headerLine.Replace("Name:", "").Trim();

            var regex = new Regex("[ ]{2,}", RegexOptions.None);
            headerLine = regex.Replace(headerLine, " ");

            List<string> splitName;

            if (headerLine.Contains(','))
                splitName = headerLine.Split(',').Reverse().ToList();
            else
                splitName = headerLine.Split().ToList();

            if (splitName.Count != 2) throw new ArgumentException(ProperFormatException, nameof(headerLine));

            var firstName = splitName[0].Trim();
            var lastName = splitName[1].Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                throw new ArgumentException(ProperFormatException, nameof(headerLine));

            var toReturn = new User
            {
                FirstName = firstName,
                LastName = lastName
            };

            var test = GetGifts(properlyQualifiedFilename, toReturn);

            toReturn.Gifts = test;

            return toReturn;
        }

        private static string GetHeader(string properlyQualifiedFilename)
        {
            var headerLine = File.ReadLines(properlyQualifiedFilename).First().Trim();

            return headerLine.StartsWith("Name:")
                ? headerLine
                : throw new ArgumentException("Header must start with \"Name:\"", nameof(headerLine));
        }

        private static List<Gift> GetGifts(string properlyQualifiedFilename, User user)
        {
            return File.ReadLines(properlyQualifiedFilename)
                .Skip(1)
                .Select(line => line.Trim())
                .Where(line => line != "")
                .Select(line => new Gift
                {
                    Title = line,
                    User = user
                })
                .ToList();
        }

        private static string GetProperlyQualifiedFilename(string fileName)
        {
            if (fileName is null) throw new NullReferenceException();

            return !File.Exists(fileName)
                ? Path.Combine(Assembly.GetExecutingAssembly().Location, fileName)
                : fileName;
        }
    }
}