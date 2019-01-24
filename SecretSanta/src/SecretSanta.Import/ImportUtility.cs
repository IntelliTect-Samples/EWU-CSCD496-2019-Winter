using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (string.IsNullOrEmpty(fileName)) throw new NullReferenceException();

            var headerLine = GetHeader(fileName);

            headerLine = headerLine.Replace("Name: ", "").Trim();

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

            return new User
            {
                FirstName = firstName,
                LastName = lastName
            };
        }

        private static string GetHeader(string fileName)
        {
            var headerLine = File.ReadLines(fileName).First().Trim();

            return headerLine.StartsWith("Name:")
                ? headerLine
                : throw new ArgumentException("Header must start with \"Name:\"", nameof(headerLine));
        }
    }
}