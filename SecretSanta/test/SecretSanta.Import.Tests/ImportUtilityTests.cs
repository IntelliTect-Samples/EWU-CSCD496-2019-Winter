using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class ImportUtilityTests
    {
        private List<string> CreatedFiles { get; } = new List<string>();

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var cur in CreatedFiles) File.Delete(cur);
        }

        private string WriteTemporaryFile(IEnumerable<string> toWrite)
        {
            var tempFileName = Path.GetTempFileName();

            using (var streamWriter = new StreamWriter(tempFileName))
            {
                foreach (var cur in toWrite)
                {
                    streamWriter.WriteLine(cur);
                    CreatedFiles.Add(tempFileName);
                }

                streamWriter.Close();
            }

            return tempFileName;
        }

        [DataTestMethod]
        [DataRow("Name: Inigo Montoya", "Inigo", "Montoya")]
        [DataRow("Name: Princess Buttercup", "Princess", "Buttercup")]
        [DataRow("Name: Person McName", "Person", "McName")]
        [DataRow("Name: Person Hyphen-Name", "Person", "Hyphen-Name")]
        public void Import_HeaderWithFirstnameLastname_ProperFormat_Success(string toWrite, string firstName,
            string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }

        [DataTestMethod]
        [DataRow("Name: Montoya, Inigo", "Inigo", "Montoya")]
        [DataRow("Name: Buttercup, Princess", "Princess", "Buttercup")]
        [DataRow("Name: McName, Person", "Person", "McName")]
        [DataRow("Name: Hyphen-Name, Person", "Person", "Hyphen-Name")]
        public void Import_HeaderWithLastnameCommaFirstname_ProperFormat_Success(string toWrite, string firstName,
            string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }

        [TestMethod]
        public void Import_HeaderWithLastnameFirstName_TooManySpaces_Success()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name:     Inigo     Montoya "});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
        }

        [TestMethod]
        public void Import_HeaderWithLastnameCommaFirstname_TooManySpaces_Success()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name:     Montoya    ,     Inigo "});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
        }

        [TestMethod]
        public void Import_HeaderWithMalformedNameLineStarter_LeadingSpaces_Success()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"   Name:     Inigo     Montoya "});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithMalformedNameLineStarter_SpacesBetweenWordAndColon_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name :     Inigo     Montoya "});

            ImportUtility.Import(tempFileName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Import_FilenameEmptyOrNull_ArgumentException(string fileName)
        {
            ImportUtility.Import(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithTooManyNamesSeparatedBySpaces_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name: Inigo Montoya Princess Buttercup"});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithTooManyNamesWithCommas_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name: Montoya, Inigo Buttercup, Princess"});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithNoNamesAllCommas_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name: ,,,,"});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithNoNamesJustSpaces_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name:   "});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithCharFollowedBySpaces_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name: a  "});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_HeaderWithSingleSpace_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name: "});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_OnlyHeader_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"Name:"});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_MissingHeaderWithComma_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {" , "});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_MissingHeaderWithSpaces_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {"       "});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Import_EmptyFile_ArgumentException()
        {
            var tempFileName = WriteTemporaryFile(new List<string> {""});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Import_FileDoesNotExist_FileNotFoundException()
        {
            ImportUtility.Import("doesNotExist.txt");
        }
    }
}