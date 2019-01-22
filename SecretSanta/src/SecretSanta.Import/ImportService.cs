using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace SecretSanta.Import
{
    public class ImportService
    {
        public FileStream Istream { get; set; }

        public ImportService(string path)
        {
            if (File.Exists(path))
            {
                Istream = new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            else
            {
                throw new FileNotFoundException("File does not exist error");
            }
        }

        public string ReadName()
        {
            StreamReader streamReader = new StreamReader(Istream);
            string name = streamReader.ReadLine();

            return name;
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
                throw new FormatException("The word passed in does not meet the define standard.");
            }

            if(temp.Length == 2)
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

            if (temp.Length != 2)
            {
                throw new FormatException("String Parsing error");
            }

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
    }
}
