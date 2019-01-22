using src.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{
    public class LibraryClass
    {
        static void Main(string[] args)
        {

        }

        public static bool IsValidAbsolutePathFile(in string path)
        {
            IfCustomNullException(path, "Cannot check if File Exists with Absolute path with Null reference");
            return File.Exists(path);
        }

        public static bool IsValidFileFromBin(in string path)
        {
            IfCustomNullException(path, "Cannot check if File Exists with Local path from bin with Null reference");
            return IsValidAbsolutePathFile(path);
        }

        public static bool IsValidFileFromActualProgram(in string path)
        {
            IfCustomNullException(path, "Cannot check if File Exists with Local path with Null reference");

            string directoryPath = System.Environment.CurrentDirectory;
            string resultPath = Path.Combine(directoryPath, @"..\..\..\", path);
            string actualResultPath = Path.GetFullPath(resultPath);
            return File.Exists(actualResultPath);
        }

        public static StreamReader MakeStreamReader(in string path)
        {
            IfCustomNullException(path, "Cannot make a StreamReader with Null reference");

            StreamReader reader;
            try
            {
                reader = new StreamReader(path);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"{e}: ");
            }

            return reader;
            
        }

        public static void IfCustomNullException(in string path, in string customMessage)
        {
            if (path is null)
            {
                throw new ArgumentNullException(customMessage);
            }
        }

        public static bool IsStringNull(in string str)
        {
            return str is null;
        }

        public static string[] TrimStringArray(in string str)
        {
            IfCustomNullException(str, "String cannot be null when attempting to convert to trimmed array.");
            string[] stringArray = str.Split(" ");
            for(int index = 0; index < stringArray.Length; index++)
            {
                stringArray[index] = stringArray[index].Trim();
            }
            return stringArray;
        }

        public static User CreateUserFirstLast(in string firstLastLine)
        {
            IfCustomNullException(firstLastLine, "String cannot be null when attempting to create user with format Name:First Last.");
            string[] firstLastLineArray = TrimStringArray(firstLastLine);
            User newUser = new User
            {
                FirstName = firstLastLineArray[1],
                LastName = firstLastLineArray[2],
                GiftList = new List<Gift>()
            };//probably better to have constructor
            return newUser;
        }

        public static User CreateUserLastFirst(in string lastFirstLine)
        {
            IfCustomNullException(lastFirstLine, "String cannot be null when attempting to create user with format Name:Last, First.");
            string[] lastFirstLineArray = TrimStringArray(lastFirstLine);
            User newUser = new User
            {
                LastName = lastFirstLineArray[1].Substring(0, lastFirstLineArray.Length - 1),
                FirstName = lastFirstLineArray[2],
                GiftList = new List<Gift>()
            };//probably better to have constructor
            return newUser;
        }

        private static bool IsValidLine(in string firstLine, Func<string, bool> order, string customMessage)
        {
            IfCustomNullException(firstLine, $"Line contains incorrect format for User by {customMessage}");

            bool result = false;
            string trimFirstLine = firstLine.Trim();
            if (trimFirstLine.StartsWith("Name:"))
            {
                string[] firstLineArray = trimFirstLine.Split(" ");
                if (firstLineArray.Length == 3)
                {
                    if (order(firstLineArray[1]))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public static bool IsValidFirstLast(in string firstLine)
        {
            IfCustomNullException(firstLine, "String cannot be null when checking if valid user in format Name: First Last");
            return IsValidLine(firstLine, IsNotEndWithComma, "Name: FirstName LastName");
        }

        public static bool IsValidLastFirst(in string firstLine)
        {
            IfCustomNullException(firstLine, "String cannot be null when checking if valid user in format Name: Last, First");
            return IsValidLine(firstLine, IsEndWithComma, "Name: LastName, FirstName");
        }

        public static bool IsEndWithComma(string str) //can't use in with function?
        {
            IfCustomNullException(str, "String cannot be null when checking if ends with comma.");
            return str.EndsWith(',');
        }

        public static bool IsNotEndWithComma(string str)
        {
            //IfCustomNullException(str, "String cannot be null when checking if ends with comma.");
            return !(IsEndWithComma(str));
        }

        public static bool StreamHasNextLine(StreamReader reader)
        {
            return reader.Peek() >= 0;
        }

        public static void AddWishList(User user, StreamReader reader)
        {
            string tempLine;
            Gift gift;
            while (reader.Peek() >= 0)
            {
                tempLine = reader.ReadLine().Trim();
                if (tempLine.Length > 0)
                {
                    gift = new Gift
                    {
                        Title = tempLine
                    };//probably better to have constructor
                    user.GiftList.Add(gift);
                }
            }
        }

        public static User CreateUserAndPopulateList(string validPath)
        {
            StreamReader reader = MakeStreamReader(validPath);
            User user = null; //bad practice. Probably need to fix
            if (StreamHasNextLine(reader))
            {
                String str = reader.ReadLine();
                if (IsValidFirstLast(str))
                {
                    user = CreateUserFirstLast(str);
                }
                else if (IsValidLastFirst(str))
                {
                    user = CreateUserLastFirst(str);
                }
                else
                {
                    reader.Close();
                    return user;
                    //should throw exception
                }
                AddWishList(user, reader);
            }
            reader.Close();
            return user;
        }
    }
}
