using System;
using System.IO;

namespace SecretSanta.UserGiftImport
{
    public class UserGiftImporter
    {
        public StreamReader StreamReader { get; set; }

        public void Open(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException();
            }
            else if (!File.Exists(path))
            {
                throw new ArgumentException("File does not exist.");
            }
            else if (File.Exists(path))
            {
                StreamReader = new StreamReader(path);
            }
        }

        public void Close()
        {
            if (StreamReader != null)
            {
                StreamReader.Close();
                StreamReader = null;
            }

        }

        public string ReadNext()
        {
            return StreamReader.ReadLine();
        }
    }
}
