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
        [DataRow("Name:     Inigo     Montoya ", "Inigo", "Montoya")]
        [DataRow("   Name:     Inigo     Montoya ", "Inigo", "Montoya")]
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
        [DataRow("Name:     Montoya    ,     Inigo ", "Inigo", "Montoya")]
        public void Import_HeaderWithLastnameCommaFirstname_ProperFormat_Success(string toWrite, string firstName,
            string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [ExpectedException(typeof(NullReferenceException))]
        public void Import_FilenameEmptyOrNull_NullReferenceException(string fileName)
        {
            ImportUtility.Import(fileName);
        }

        [DataTestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("Name: Inigo Montoya Princess Buttercup")]
        [DataRow("Name: Montoya, Inigo Buttercup, Princess")]
        [DataRow("Name: ,,,,")]
        [DataRow("Name:   ")]
        [DataRow("Name: a  ")]
        [DataRow("Name: ")]
        [DataRow("Name:")]
        [DataRow("Name :     Inigo     Montoya ")]
        [DataRow(" , ")]
        [DataRow("       ")]
        [DataRow("")]
        public void Import_MalformedInput_ArgumentException(string toWrite)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

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