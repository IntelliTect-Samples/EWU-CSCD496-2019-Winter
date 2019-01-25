using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment2FileLibrary
{
    public class FileImportAndUse : IDisposable
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
            else
            {
                Close();
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

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
            }
        }

        /*public Boolean ValidateHeader()
        {
            if (StreamReader == null)
            {
                throw new
            }
            return false;
        }*/
    }
}
