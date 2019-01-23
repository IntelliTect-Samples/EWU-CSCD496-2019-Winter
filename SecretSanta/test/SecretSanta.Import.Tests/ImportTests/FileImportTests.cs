using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import.Import;
using System;
using System.Collections.Generic;
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
            if (TempFileName != null) File.Delete(Path.Combine(TempFileDirectory, TempFileName));

            TempFileDirectory = Path.GetTempPath();
            TempFileName = Path.GetTempFileName();
        }

        [TestCleanup]
        public void DeleteTestFile()
        {
            File.Delete(Path.Combine(TempFileDirectory, TempFileName));
        }

        private void WriteLinesToTempFile(string[] lines)
        {
            File.WriteAllLines(Path.Combine(TempFileDirectory, TempFileName), lines);
        }

        [TestMethod]
        public void ReadFileFormat_FirstName_LastName()
        {
            WriteLinesToTempFile(new string[] { "Name: Alan Watts" });

            (User user, List<Gift> gifts) = FileImportService.ImportFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);
        }

        [TestMethod]
        public void ReadFileFormat_LastName_Comma_FirstName()
        {
            WriteLinesToTempFile(new string[] { "Name: Watts, Alan" });

            (User user, List<Gift> gifts) = FileImportService.ImportFile(TempFileName);

            Assert.AreEqual("Alan", user.FirstName);
            Assert.AreEqual("Watts", user.LastName);
        }

        [TestMethod]
        public void ReadFileFormat_ValidUser_3Gifts()
        {
            string[] lines = new string[]
            {
                "Name: Alan Watts",
                "Nintendo Switch",
                "Tesla",
                "Dog"
            };

            WriteLinesToTempFile(lines);

            (User user, List<Gift> gifts) = FileImportService.ImportFile(TempFileName);

            Assert.AreEqual("Nintendo Switch", gifts[0].Title);
            Assert.AreEqual("Tesla", gifts[1].Title);
            Assert.AreEqual("Dog", gifts[2].Title);
        }

        [TestMethod]
        public void ReadFileFormat_ValidUser_EmptyLinesBetweenValidGifts()
        {
            string[] lines = new string[]
            {
                "Name: Alan Watts",
                "Nintendo Switch",
                " ",
                "Tesla",
                " ",
                "Dog",
                " "
            };

            WriteLinesToTempFile(lines);

            (User user, List<Gift> gifts) = FileImportService.ImportFile(TempFileName);

            Assert.AreEqual("Nintendo Switch", gifts[0].Title);
            Assert.AreEqual("Tesla", gifts[1].Title);
            Assert.AreEqual("Dog", gifts[2].Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadFile_WithEmptyHeader()
        {
            WriteLinesToTempFile(new string[] { string.Empty });
            FileImportService.ImportFile(TempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadHeader_PassedInNull()
        {
            FileImportService.ImportFile(null);
        }
    }
}
