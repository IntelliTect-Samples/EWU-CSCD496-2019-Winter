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
        public string TempFileDirectory { get; set; }

        [TestInitialize]
        public void CreateTestFile()
        {
            TempFileDirectory = Path.GetTempPath();
            TempFileName = Path.GetTempFileName(); // if this exist, ask for another one
            FileInfo fileInfo = new FileInfo(TempFileName);
            fileInfo.Attributes = FileAttributes.Temporary;
        }

        private void WriteLineToTempFile(string line)
        {
            using (StreamWriter sw = File.CreateText(TempFileName))
            {
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();
            }
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
            WriteLineToTempFile(string.Empty);
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
