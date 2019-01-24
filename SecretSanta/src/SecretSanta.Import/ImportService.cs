using System;
using System.IO;

namespace SecretSanta.Import
{
    public class ImportService : IDisposable
    {
        public static int InstanceCount { get; set; }
        public FileStream Istream { get; private set; }

        public ImportService(string path)
        {
            if (File.Exists(path))
            {
                Istream = new FileStream(path, FileMode.Open, FileAccess.Read);
                InstanceCount++;
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

        public string ReadName()
        {
            StreamReader streamReader = new StreamReader(Istream);
            return streamReader.ReadLine();
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
