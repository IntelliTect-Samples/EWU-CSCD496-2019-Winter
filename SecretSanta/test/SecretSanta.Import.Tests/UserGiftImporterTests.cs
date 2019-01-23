using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SecretSanta.UserGiftImport.Tests
{
    [TestClass]
    public class UserGiftImporterTests
    {
        public UserGiftImporter Importer { get; set; }
        public string TestFilePath { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            TestFilePath = System.IO.Path.GetTempFileName();
            DeleteFile();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DeleteFile();
        }

        private void CreateWriteToFile(string[] lines)
        {
            File.WriteAllLines(TestFilePath, lines);
        }

        private void DeleteFile()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Open_NullPath_ArgumentNullException()
        {
            using (Importer = new UserGiftImporter())
            {
                Importer.Open(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Open_NonExistingFile_ArgumentException()
        {
            using (Importer = new UserGiftImporter())
            {
                Importer.Open("nonExistingFile.txt");
            }
        }

        [TestMethod]
        public void Open_ExistingFile_OpenStream()
        {
            string[] lines = new string[] { };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                Importer.Open(TestFilePath);

                Assert.AreEqual(TestFilePath, ((FileStream)Importer.StreamReader.BaseStream).Name);
            }
                
            DeleteFile();
        }
        
        [TestMethod]
        public void ReadNext_BlankLine_ReturnNull()
        {
            string[] lines = new string[] { };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                Importer.Open(TestFilePath);
                string line = Importer.ReadNext();

                Assert.AreEqual(line, null);
            }

            DeleteFile();
        }

        [TestMethod]
        public void ReadNext_ValidLine_ReturnLine()
        {
            string[] lines = new string[] { "First line of the file" };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                Importer.Open(TestFilePath);
                string line = Importer.ReadNext();

                Assert.AreEqual("First line of the file", line);
            }

            DeleteFile();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExtractHeader_NullLine_ArgumentNullException()
        {
            using (Importer = new UserGiftImporter())
            {
                string[] header = Importer.ExtractHeader(null);
            }
        }

        [TestMethod]
        public void ExtractHeader_ValidLineFirstNameAppearsFirst_ReturnHeader()
        {
            using (Importer = new UserGiftImporter())
            {
                string[] header = Importer.ExtractHeader("Name: Richard Teller");
                Assert.AreEqual("Name", header[0]);
                Assert.AreEqual("Richard", header[1]);
                Assert.AreEqual("Teller", header[2]);
            }
        }

        [TestMethod]
        public void ExtractHeader_ValidLineFirstNameAppearsSecond_ReturnHeader()
        {
            using (Importer = new UserGiftImporter())
            {
                string[] header = Importer.ExtractHeader("Name: Teller, Richard");
                Assert.AreEqual("Name", header[0]);
                Assert.AreEqual("Richard", header[1]);
                Assert.AreEqual("Teller", header[2]);
            }
        }

        [TestMethod]
        public void ReadGifts_ValidGifts_ReturnListOfGiftsNoBlanks()
        {
            string[] lines = new string[] { "Gift1", "", "Gift2", "", "Gift3" };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                Importer.Open(TestFilePath);
                List<string> gifts = Importer.ReadGifts();

                Assert.AreEqual(3, gifts.Count);
                Assert.AreEqual("Gift1", gifts[0]);
                Assert.AreEqual("Gift2", gifts[1]);
                Assert.AreEqual("Gift3", gifts[2]);
            }

            DeleteFile();
        }

        [TestMethod]
        public void ReadGifts_NoGifts_ReturnListNoItems()
        {
            string[] lines = new string[] { "", "", "", "", "" };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                Importer.Open(TestFilePath);
                List<string> gifts = Importer.ReadGifts();

                Assert.AreEqual(0, gifts.Count);
            }

            DeleteFile();
        }

        [TestMethod]
        public void PopulateUser_ValidFile_UserAdded()
        {
            string[] lines = new string[] { "Name: Richard Teller", "Gift1", "Gift2", "", "Gift3" };
            CreateWriteToFile(lines);

            using (Importer = new UserGiftImporter())
            {
                User user = Importer.PopulateUser(TestFilePath);

                Assert.AreEqual(user.FirstName, "Richard");
                Assert.AreEqual(user.LastName, "Teller");
                Assert.AreEqual(3, user.Gifts.Count);

                List<Gift> gifts = new List<Gift>(user.Gifts);
                Assert.AreEqual("Gift1", gifts[0].Title);
                Assert.AreEqual("Gift2", gifts[1].Title);
                Assert.AreEqual("Gift3", gifts[2].Title);
            }

            DeleteFile();
        }
    }
}
