using System;
using System.IO;

namespace SecretSanta.UserGiftImport
{
    public class UserGiftImporter : IDisposable
    {
        public StreamReader StreamReader { get; set; }

        public void Open(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException();
            }
            else if (!File.Exists(fileName))
            {
                throw new ArgumentException("File does not exist.");
            }
            else if (File.Exists(fileName))
            {
                StreamReader = new StreamReader(fileName);
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

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
            }
        }
    }
}
