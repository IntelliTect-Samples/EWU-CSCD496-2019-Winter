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

        public Boolean Close()
        {
            if (StreamReader != null)
            {
                StreamReader.Close();
                StreamReader = null;
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (StreamReader != null)
            {
                StreamReader.Dispose();
                Close();
            }
        }

        public Boolean ValidateHeader()
        {
            if (StreamReader == null)
            {
                throw new NullReferenceException("The StreamReader must be opened before " +
                    "using it to read the file and validate the header");
            }

            string firstLine = StreamReader.ReadLine();
            
            //resets reading position
            Stream s = StreamReader.BaseStream;
            s.Position = 0;

            if (firstLine == null)
            {
                return false;
            }

            String[] split = firstLine.Split();

            if (split.Length != 3)
            {
                return false;
            }
            else if (split[0].Equals("`Name:") && !split[1].EndsWith(",")
                || split[0].Equals("Name:") && split[1].EndsWith(","))
            {
                return true;
            }
            
            return false;
        }
        
    }
}
