using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using Library;
using System.IO;
using System;

namespace LibraryTests
{
    [TestClass]
    public class LibraryTests
    {
        public string TempFileName { get; set; }
        public string TempFileLocation { get; set; }

        /*[TestInitialize]
        public void SetUpTestFile()
        {
            TempFileLocation = Path.GetTempPath();
            TempFileName = Path.GetTempFileName();
            FileInfo fileInfo = new FileInfo(TempFileName);
            fileInfo.Attributes = FileAttributes.Temporary;

            StreamWriter reader = File.AppendText(TempFileName);
            reader.WriteLine("Carrie White");
            reader.Flush();
            reader.Close();
        }*/

        [DataRow(@"C:\Users\User\Desktop\EWU-CSCD496-2019-Winter-Assignment1 (3)\EWU-CSCD496-2019-Winter-Assignment1\SecretSanta\LibraryTests\FileAb.txt")]
        [TestMethod]
        public void IsValidTestFile_AbsolutePath(string path)
        {
            bool result = File.Exists(path);
            Assert.IsTrue(result);
        }

        //test if the file is located in netcore2.2
        [DataRow(@".\FileBin.txt")]
        [TestMethod]
        public void IsValidTestFile_FromBinPath_LocalPath(string path)
        {
            bool result = File.Exists(path);
            Assert.IsTrue(result);
        }

        [DataRow(@"FileAb.txt")]
        [TestMethod]
        public void IsValidTestFile_NagivateToActualProgramPath_LocalPath(string path)
        {
            string directoryPath = System.Environment.CurrentDirectory;
            string resultPath = Path.Combine(directoryPath , @"..\..\..\", path);
            string actualResultPath = Path.GetFullPath(resultPath);
            bool result = File.Exists(actualResultPath);
            Assert.IsTrue(result);
        }

        [DataRow(@"C:\Users\User\Desktop\EWU-CSCD496-2019-Winter-Assignment1 (3)\EWU-CSCD496-2019-Winter-Assignment1\SecretSanta\LibraryTests\FileAb.txt")]
        [TestMethod]
        public void IsValidFile_NagivateToActualProgramPath_AbsolutePath(string path)
        {
            bool result = LibraryClass.IsValidAbsolutePathFile(path);
            Assert.IsTrue(result);
        }

        [DataRow(@"FileActual.txt")]
        [TestMethod]
        public void IsValidFile_FromCurrent_LocalPath(string path)
        {
            bool result = LibraryClass.IsValidFileFromActualProgram(path);
            Assert.IsTrue(result);
        }

        [DataRow(@"FileActual.txt")]
        [TestMethod]
        public void IsValid_StreamReader(string path)
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(path);
            Assert.IsNotNull(tempReader);
        }

        [DataRow(null)]
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Invalid_StreamReader_NullArguementException(string path)
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(path);
            Assert.IsNull(tempReader);
        }

        [DataRow("./gibberish")]
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Invalid_StreamReader_InvalidArguement(string path)
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(path);
            Assert.IsNotNull(tempReader);
        }

        [DataRow(null, true)]
        [DataRow("Jack London", false)]
        [TestMethod]
        public void Invalid_CheckIsString(string str, bool ans)
        {
            bool result = LibraryClass.IsStringNull(str);
            Assert.AreEqual(result, ans);
        }

        [DataRow("Name: Jacob Eeping")]
        [TestMethod]
        public void Valid_ReadLine_FirstNameLastName(string line)
        {
            User tempUser = LibraryClass.CreateUserFirstLast(line);
            Assert.AreEqual(tempUser.FirstName, "Jacob");
            Assert.AreEqual(tempUser.LastName, "Eeping");
        }

        [DataRow("Name: Jacob Eeping")]
        [TestMethod]
        public void Valid_ReadLine_IsFirstNameLastName(string line)
        {
            bool result = LibraryClass.IsValidFirstLast(line);
            Assert.IsTrue(result);
        }

        public void Valid_EndsWithComma(string str)
        {
            bool result = LibraryClass.IsEndWithComma(str);
            Assert.IsTrue(result);
        }
    }
}
