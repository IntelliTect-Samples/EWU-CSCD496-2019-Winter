using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

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
                    streamWriter.WriteLine(cur);

                streamWriter.Close();
            }

            CreatedFiles.Add(tempFileName);

            return tempFileName;
        }

        private string WriteFileAtPath(string path, IEnumerable<string> toWrite)
        {
            for (var i = 0; i < 100; i++) // attempt to create random file 100 times before giving up
            {
                var randomFileName = Path.GetRandomFileName();

                var newFileName = Path.Combine(path, randomFileName);

                if (File.Exists(newFileName)) continue;

                using (var streamWriter = File.CreateText(newFileName))
                {
                    foreach (var cur in toWrite)
                        streamWriter.WriteLine(cur);

                    streamWriter.Close();
                }

                CreatedFiles.Add(newFileName);
                return newFileName;
            }

            throw new IOException("Random file could not be created");
        }

        [DataTestMethod]
        [DataRow("Name: Inigo Montoya", "Inigo", "Montoya")]
        [DataRow("Name: Princess Buttercup", "Princess", "Buttercup")]
        [DataRow("Name: Person McName", "Person", "McName")]
        [DataRow("Name: Person Hyphen-Name", "Person", "Hyphen-Name")]
        [DataRow("Name:     Inigo     Montoya ", "Inigo", "Montoya")]
        [DataRow("   Name:     Inigo     Montoya ", "Inigo", "Montoya")]
        public void Import_TestHeader_WithFirstnameLastname_ProperFormat_Success(string toWrite, string firstName,
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
        public void Import_TestHeader_WithLastnameCommaFirstname_ProperFormat_Success(string toWrite, string firstName,
            string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }

        [DataTestMethod]
        [DataRow("Name: Montoya, Inigo", "Roomba,Echo Show,Broom", "Inigo", "Montoya")]
        [DataRow("Name: Inigo Montoya", "Roomba", "Inigo", "Montoya")]
        [DataRow("Name: Inigo Montoya", "", "Inigo", "Montoya")]
        public void Import_WithFullGiftList_ProperFormat_Success(string headerLine, string giftsToUse, string firstName,
            string lastName)
        {
            var testUser = new User
            {
                FirstName = firstName,
                LastName = lastName
            };
            var gifts = giftsToUse
                .Split(',')
                .Select(line => line.Trim())
                .Where(line => line != "")
                .Select(line => new Gift
                {
                    Title = line,
                    User = testUser
                })
                .ToList();

            var toWrite = giftsToUse.Split(',').ToList();
            toWrite.Insert(0, headerLine);

            var tempFileName = WriteTemporaryFile(toWrite);

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);

            // Using for instead of .SequenceEquals so I can see which in the sequence is failing
            for (var i = 0; i < gifts.Count; i++) Assert.AreEqual(gifts[i].Title, user.Gifts[i].Title);
        }

        [DataTestMethod]
        [DataRow("./")]
        [DataRow("../")]
        [DataRow("")]
        public void Import_FileAtRelativePath_Success(string path)
        {
            var tempFileName = WriteFileAtPath(path, new List<string> {"Name: Inigo Montoya"});

            var user = ImportUtility.Import(tempFileName);

            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
        }

        [DataTestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Import_FilenameNull_NullReferenceException()
        {
            ImportUtility.Import(null);
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
        [DataRow("")] // file empty
        public void Import_TestHeader_MalformedInput_ArgumentException(string toWrite)
        {
            var tempFileName = WriteTemporaryFile(new List<string> {toWrite});

            ImportUtility.Import(tempFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void Import_FileDoesNotExist_FileNotFoundException()
        {
            ImportUtility.Import("doesNotExist.txt");
        }
    }
}