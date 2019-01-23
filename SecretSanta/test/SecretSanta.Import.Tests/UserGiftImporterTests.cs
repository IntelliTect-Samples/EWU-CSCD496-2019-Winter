using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            TestFilePath = Environment.CurrentDirectory + "\\TestFile.txt";
            DeleteFile();
            
        }

        [TestCleanup]
        public void TestCleanup()
        {

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


        /*
        [TestMethod]
        public void MyTestMethod()
        {
            string[] lines = new string[] { "line1", "line2" };
            CreateFile(lines);

            
            string[] lines2 = File.ReadAllLines(TestFilePath);

            Assert.AreEqual("line1", lines2[0]);
            Assert.AreEqual("line2", lines2[1]);
            

            DeleteFile();
        }
        */

    }
}
