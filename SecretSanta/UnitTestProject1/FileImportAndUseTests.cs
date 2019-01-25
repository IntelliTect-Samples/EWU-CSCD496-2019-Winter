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
                Assert.IsNotNull(f.StreamReader);
            }
        }

        [TestMethod]
        public void FileImportAndUse_OpenSameFileWhileAlreadyOpen()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                f.Open(TempFile);
                Assert.IsNotNull(f.StreamReader);
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
                Assert.IsNotNull(f.StreamReader);
            }

            File.Delete(TempFile2);
        }

        [TestMethod]
        public void MyFileImportAndUse_CloseWhileClosed()
        {
            using (var f = new FileImportAndUse())
            {
                Assert.IsFalse(f.Close());
            }
        }

        [TestMethod]
        public void FileImportAndUse_CloseWhileOpen()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                Assert.IsTrue(f.Close());
                Assert.IsNull(f.StreamReader);
            }
        }

        [TestMethod]
        public void FileImportAndUse_Dispose()
        {
            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                f.Dispose();
                Assert.IsNull(f.StreamReader);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FileImportAndUse_ValidateHeader_TryValidateWithClosedStreamWriter()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("`Name: Isaac Bliss");
            }

            using (var f = new FileImportAndUse())
            {
                f.ValidateHeader();
            }
        }

        [TestMethod]
        public void FileImportAndUse_ValidateHeader_TryValidateWithIncorrectNumberOfWords()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("`Name: Isaac Edwin Bliss");
            }

            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                Assert.IsFalse(f.ValidateHeader());
            }
        }

        [TestMethod]
        public void FileImportAndUse_ValidateHeader_TryValidateWithCorrectNumberOfWords_WrongFormat()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("`Name: Isaac, Bliss");
            }

            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                Assert.IsFalse(f.ValidateHeader());
            }
        }

        [TestMethod]
        public void FileImportAndUse_ValidateHeader_TryValidateWithCorrectNumberOfWords_CorrectFormat_v1()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("`Name: Isaac Bliss");
            }

            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                Assert.IsTrue(f.ValidateHeader());
            }
        }

        [TestMethod]
        public void FileImportAndUse_ValidateHeader_TryValidateWithCorrectNumberOfWords_CorrectFormat_v2()
        {
            using (var sw = new StreamWriter(TempFile))
            {
                sw.WriteLine("Name: Bliss, Isaac");
            }

            using (var f = new FileImportAndUse())
            {
                f.Open(TempFile);
                Assert.IsTrue(f.ValidateHeader());
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            File.Delete(TempFile);
        }

       
    }

}
