using System.IO;
using System.Linq;
using SecretSanta.Domain.Models;

namespace SecretSanta.Library
{
    public class FileParser
    {
        public static User ParseWishlistFile(string wishlistFileName)
        {
            User user;
            using (var streamReader = new StreamReader(FileUtility.OpenFile(wishlistFileName)))
            {
                user = ParseWishlistHeader(streamReader);
            }

            return user;
        }

        private static User ParseWishlistHeader(StreamReader fin)
        {
            string line;
            while (string.IsNullOrWhiteSpace(line = fin.ReadLine()))
            {
                if(fin.EndOfStream) throw new InvalidDataException("File does not contain a header");
            }
            
            if(!line.Contains("Name"))
                throw new InvalidDataException($"Header: \"{line}\" is an improper format");

            var swapNames = line.Contains(',');
            var names = line.Split().Skip(1).ToArray();
            if(names.Length != 2)
                throw new InvalidDataException($"Header: \"{line}\" does not contain a proper user name");

            var firstname = swapNames ? names[1] : names[0];
            var lastname = swapNames ? names[0].Remove(names[0].Length - 1) : names[1];

            return new User {FirstName = firstname, LastName = lastname};
        }
    }
}