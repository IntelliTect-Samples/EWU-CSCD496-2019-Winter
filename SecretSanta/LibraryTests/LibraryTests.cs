using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using Library;
using System.IO;
using System;
using System.Collections.Generic;

namespace LibraryTests
{
    [TestClass]
    public class LibraryTests
    {
        public string TempFileName { get; set; }
        public string TempFileLocation { get; set; }
        public string TempFilePath { get; set; }
        public FileInfo FileInfo { get; set; }

        [TestInitialize]
        public void SetUpTestFile()
        {
            TempFileLocation = Path.GetTempPath();
            TempFileName = Path.GetTempFileName();
            TempFilePath = CreateTempFilePath();
            FileInfo = new FileInfo(TempFileName)
            {
                Attributes = FileAttributes.Temporary
            };
        }

        [TestCleanup]
        public void CleanUpTestFiles()
        {
        }

        private string CreateTempFilePath()
        {
            return Path.Combine(TempFileLocation, @"/", TempFileName);
        }

        private void SetUpByWriteToTemp(string line)
        {
            StreamWriter reader = File.AppendText(TempFileName);
            reader.WriteLine(line);
            reader.Flush();
            reader.Close();
        }

        //[DataRow(@"C:\Users\User\Desktop\EWU-CSCD496-2019-Winter-Assignment1 (3)\EWU-CSCD496-2019-Winter-Assignment1\SecretSanta\LibraryTests\FileAb.txt")]
        [TestMethod]
        public void IsValidTestFile_AbsolutePath()
        {
            bool result = File.Exists(CreateTempFilePath());
            Assert.IsTrue(result);
        }

        //test if the file is located in netcore2.2
        //[DataRow(@".\FileBin.txt")]
        [TestMethod]
        public void IsValidTestFile_FromBinPath_LocalPath()
        {
            bool result = File.Exists(TempFileName);
            Assert.IsTrue(result);
        }

        /*[DataRow(@"FileAb.txt")]
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
        */

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void IsValidFile_NagivateToActualProgramPath_AbsolutePathIsNull()
        {
            bool result = LibraryClass.IsValidAbsolutePathFile(null);
            Assert.IsTrue(result);
        }

        //[DataRow(@"FileActual.txt")]
        [TestMethod]
        public void IsValidFile_FromCurrent_LocalPath()
        {
            bool result = LibraryClass.IsValidFileFromActualProgram(TempFileName);
            Assert.IsTrue(result);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void InvalidFile_FromCurrent_LocalPathIsNull()
        {
            bool result = LibraryClass.IsValidFileFromActualProgram(null);
            Assert.IsTrue(result);
        }

        //[DataRow(@"FileActual.txt")]
        [TestMethod]
        public void IsValid_StreamReader()
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(TempFileName);
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

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_StreamReader_NullArguement()
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(null);
            Assert.IsNotNull(tempReader);
        }

        [TestMethod]
        public void Valid_StreamReader_InvalidArguement()
        {
            StreamReader tempReader = LibraryClass.MakeStreamReader(TempFileName);
            Assert.IsNotNull(tempReader);
        }

        [TestMethod]
        public void Valid_TrimStringArray_ExcessLengths()
        {
            string inital = " a b c ";
            string[] initialArray = LibraryClass.TrimStringToArray(inital);
            string[] expected = new string[] { "a", "b", "c" };
            for (int index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(initialArray[index], expected[index]);
            }
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_TrimStringArray_ExcessLengths()
        {
            string inital = null;
            string[] initialArray = LibraryClass.TrimStringToArray(inital);
            string[] expected = new string[] { "a", "b", "c" };
            for (int index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(initialArray[index], expected[index]);
            }
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

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_ReadLine_FirstNameLastNameNull(string line)
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

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Valid_ReadLine_IsFirstNameLastNameNull(string line)
        {
            bool result = LibraryClass.IsValidFirstLast(line);
            Assert.IsTrue(result);
        }

        [DataRow("Name: Jacob,")]
        [TestMethod]
        public void Valid_EndsWithComma(string str)
        {
            bool result = LibraryClass.IsEndWithComma(str);
            Assert.IsTrue(result);
        }

        [DataRow("Name: Jacob")]
        [TestMethod]
        public void Invalid_EndsWithComma(string str)
        {
            bool result = LibraryClass.IsEndWithComma(str);
            Assert.IsFalse(result);
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_EndsWithCommaNull(string str)
        {
            bool result = LibraryClass.IsEndWithComma(str);
        }

        [DataRow("Name: Jacob,")]
        [TestMethod]
        public void Valid_NotEndsWithComma(string str)
        {
            bool result = LibraryClass.IsNotEndWithComma(str);
            Assert.IsFalse(result);
        }

        [DataRow("Name: Jacob")]
        [TestMethod]
        public void Invalid_NotEndsWithComma(string str)
        {
            bool result = LibraryClass.IsNotEndWithComma(str);
            Assert.IsTrue(result);
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_NotEndsWithCommaNull(string str)
        {
            bool result = LibraryClass.IsNotEndWithComma(str);
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_SteamHasNextLineNull(StreamReader str)
        {
            LibraryClass.StreamHasNextLine(str);
            Assert.AreEqual(2, 3);
        }

        [TestMethod]
        public void Valid_SteamHasNextLine_True()
        {
            SetUpByWriteToTemp(TempFilePath);
            StreamReader reader = LibraryClass.MakeStreamReader(TempFilePath);
            bool result = LibraryClass.StreamHasNextLine(reader);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Valid_SteamHasNextLine_False()
        {
            StreamReader reader = LibraryClass.MakeStreamReader(TempFilePath);
            bool result = LibraryClass.StreamHasNextLine(reader);
            Assert.IsFalse(result);
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_AddWishListNull_NullUser(User user)
        {
            StreamReader reader = new StreamReader(TempFilePath);
            LibraryClass.AddWishList(user, reader);
            Assert.AreEqual(2, 3);
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_AddWishListNull_NullStreamReader(StreamReader reader)
        {
            User user = new User();
            LibraryClass.AddWishList(user, reader);
            Assert.AreEqual(2, 3);
        }

        [TestMethod]
        public void Valid_AddWishList_FullListFile()
        {
            User user = new User {
                FirstName = "Daniel",
                LastName = "Torrance",
                GiftList = new List<Gift>()
            };
            SetUpByWriteToTemp("Train");
            SetUpByWriteToTemp("Books");
            SetUpByWriteToTemp("Redrum");
            StreamReader reader = new StreamReader(TempFilePath);
            LibraryClass.AddWishList(user, reader);
            Assert.IsTrue(user.GiftList.Count == 3);
            Assert.IsTrue(user.GiftList.Contains(new Gift {Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Books" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
        }

        [TestMethod]
        public void Valid_AddWishList_EmptyListFile()
        {
            User user = new User
            {
                FirstName = "Daniel",
                LastName = "Torrance",
                GiftList = new List<Gift>()
            };
            StreamReader reader = new StreamReader(TempFilePath);
            LibraryClass.AddWishList(user, reader);
            Assert.IsTrue(user.GiftList.Count == 0);
        }

        [TestMethod]
        public void Valid_AddWishList_BlankListFile()
        {
            User user = new User
            {
                FirstName = "Daniel",
                LastName = "Torrance",
                GiftList = new List<Gift>()
            };
            SetUpByWriteToTemp("   ");
            SetUpByWriteToTemp(" ");
            SetUpByWriteToTemp("");
            StreamReader reader = new StreamReader(TempFilePath);
            LibraryClass.AddWishList(user, reader);
            Assert.IsTrue(user.GiftList.Count == 0);
        }

        [TestMethod]
        public void Valid_AddWishList_PartialListFile()
        {
            User user = new User
            {
                FirstName = "Daniel",
                LastName = "Torrance",
                GiftList = new List<Gift>()
            };
            SetUpByWriteToTemp("Train");
            SetUpByWriteToTemp("   ");
            SetUpByWriteToTemp(" ");
            SetUpByWriteToTemp("");
            SetUpByWriteToTemp("Redrum");
            StreamReader reader = new StreamReader(TempFilePath);
            LibraryClass.AddWishList(user, reader);
            Assert.IsTrue(user.GiftList.Count == 2);
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
        }

        [TestMethod]
        public void Valid_ReadEntireFile_FirstLast()
        {
            SetUpByWriteToTemp("Name: Daniel Torrance");
            SetUpByWriteToTemp("Train");
            SetUpByWriteToTemp("Books");
            SetUpByWriteToTemp("Redrum");
            User user = LibraryClass.CreateUserAndPopulateList(TempFilePath);
            Assert.IsTrue(user.GiftList.Count == 3);
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Books" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
            Assert.AreEqual(user.FirstName, "Daniel");
            Assert.AreEqual(user.LastName, "Torrance");
        }

        [TestMethod]
        public void Valid_ReadEntireFile_LastFirst()
        {
            SetUpByWriteToTemp("Name: Montgomery, Monty");
            SetUpByWriteToTemp("Train");
            SetUpByWriteToTemp("Books");
            SetUpByWriteToTemp("Redrum");
            User user = LibraryClass.CreateUserAndPopulateList(TempFilePath);
            Assert.IsTrue(user.GiftList.Count == 3);
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Books" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
            Assert.AreEqual(user.FirstName, "Monty");
            Assert.AreEqual(user.LastName, "Montgomery");
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_ReadEntireFile_NullPath(string path)
        {
            User user = LibraryClass.CreateUserAndPopulateList(path);
            Assert.IsTrue(user.GiftList.Count == 3);
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Books" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
        }

        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Invalid_ReadEntireFile_InvalidHeader(string path)
        {
            SetUpByWriteToTemp("Namea: Montgomery, Monty");
            SetUpByWriteToTemp("Train");
            SetUpByWriteToTemp("Books");
            SetUpByWriteToTemp("Redrum");
            User user = LibraryClass.CreateUserAndPopulateList(TempFilePath);
            Assert.IsTrue(user.GiftList.Count == 3);
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Train" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Books" }));
            Assert.IsTrue(user.GiftList.Contains(new Gift { Title = "Redrum" }));
            Assert.AreEqual(user.FirstName, "Monty");
            Assert.AreEqual(user.LastName, "Montgomery");
        }
    }
}
