using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
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
            var streamWriter = File.CreateText(TempFileName);
            streamWriter.WriteLine(line);
            streamWriter.Flush();
            streamWriter.Close();
        }

        [TestMethod]
        public void ReadFileFormat_FirstName_LastName()
        {
            WriteLineToTempFile("Alan Watts");
            User user = FileImport.ReadHeaderFromFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);
        }

        [TestMethod]
        public void ReadFileFormat_LastName_Comma_FirstName()
        {
            WriteLineToTempFile("Watts, Alan");
            User user = FileImport.ReadHeaderFromFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);
        }

        [TestMethod]
        public void ReadFileFormat_EmptySpace_LastName_Comma_FirstName()
        {
            WriteLineToTempFile("    Watts, Alan");
            User user = FileImport.ReadHeaderFromFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);

        }

        [TestMethod]
        public void ReadFileFormat_FirstName_LastName_EmptySpace()
        {
            WriteLineToTempFile("Alan Watts    ");
            User user = FileImport.ReadHeaderFromFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadFile_WithEmptyHeader()
        {
            WriteLineToTempFile(String.Empty);
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
