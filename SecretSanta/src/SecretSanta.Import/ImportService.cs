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
    }
}
