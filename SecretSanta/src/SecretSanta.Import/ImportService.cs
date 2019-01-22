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
            Istream = new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        public string ReadName()
        {
            if(!Istream.CanRead)
            {
                throw new IOException("File Handling Error");
            }

            StreamReader streamReader = new StreamReader(Istream);
            string name = streamReader.ReadLine();

            return name;
        }

        public string[] ParseName(string word)
        {
            bool hasComma = false;
            string[] name = new string[2];

            string[] temp = word.Split(':');

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
                    throw new IOException("Missing first or last name.");
                }
            }
            else
            {
                throw new IOException("Missing name after the :");
            }

            temp[0] = temp[0].Trim();
            temp[1] = temp[1].Trim();

            if (temp.Length != 2)
            {
                throw new IOException("String Parsing error");
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
