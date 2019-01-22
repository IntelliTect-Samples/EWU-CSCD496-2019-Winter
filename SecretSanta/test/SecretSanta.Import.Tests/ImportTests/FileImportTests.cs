using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Import.Import;
using System;
using System.IO;

namespace SecretSanta.Import.Tests.FileImportTests
{
    [TestClass]
    public class FileImportTests
    {
        public string TempFileName { get; set; }
        public string TempFileLocation { get; set; }

        [TestInitialize]
        public void CreateTestFile()
        {
            TempFileLocation = Path.GetTempPath();
            TempFileName = Path.GetTempFileName();
            FileInfo fileInfo = new FileInfo(TempFileName);
            fileInfo.Attributes = FileAttributes.Temporary;
        }

        private void WriteLineToTempFile(string line)
        {
            var streamWriter = File.AppendText(TempFileName);
            streamWriter.WriteLine(line);
            streamWriter.Flush();
            streamWriter.Close();
        }

        [TestMethod]
        public void ReadFileFormat_FirstName_LastName()
        {
            WriteLineToTempFile("Alan Watts");
            FileImport.ReadHeaderFromFile(TempFileName);
        }

        [TestMethod]
        public void ReadFileFormat_LastName_Comma_FirstName()
        {
            WriteLineToTempFile("Watts, Alan");
            FileImport.ReadHeaderFromFile(TempFileName);
        }

        [TestMethod]
        public void ReadFile_WithEmptyHeader()
        {
            WriteLineToTempFile(" ");
            FileImport.ReadHeaderFromFile(TempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadHeader_PassedInNull()
        {
            FileImport.ReadHeaderFromFile(null);
        }
    }
}
