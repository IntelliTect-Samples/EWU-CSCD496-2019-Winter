using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assignment2FileLibrary.Tests
{
    [TestClass]
    public class FileImportAndUseTests
    {
        public string TempFile;

        [TestInitialize]
        public void TestInitialize()
        {
            TempFile = Path.Combine(Path.GetTempPath(), "MyFile.txt");

            File.Delete(TempFile);

            using (var sw = new StreamWriter(TempFile))
            {}
            
        }

        [TestMethod]
        public void BasicFileExistTest()
        {
            Assert.IsTrue(File.Exists(TempFile));            
        }

        [TestMethod]
        public void TestToSeeIfThereIsBasicTextInTheFile()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("Test line");
            }

            using (var sr = new StreamReader(TempFile))
            {
                string line = sr.ReadLine();

                Assert.AreEqual("Test line", line);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileImportAndUse_Open_Null()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileImportAndUse_Open_NoFileFound()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open("wer.txt");
            }
        }

        [TestMethod]
        public void FileImportAndUse_Open_Success()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
            }
        }

        [TestMethod]
        public void FileImportAndUse_OpenSameFileWhileAlreadyOpen()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                f.Open(TempFile);
            }
        }

        [TestMethod]
        public void FileImportAndUse_OpenDifferentFileWhileAlreadyOpen()
        {
            //create a second temp file
            string TempFile2 = Path.Combine(Path.GetTempPath(), "MyFile2.txt");
            using (var sw = new StreamWriter(TempFile2))
            { }

            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                f.Open(TempFile2);
            }

            File.Delete(TempFile2);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            File.Delete(TempFile);
        }

       
    }

}
