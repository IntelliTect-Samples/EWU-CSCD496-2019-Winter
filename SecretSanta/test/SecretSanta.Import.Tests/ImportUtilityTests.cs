using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            foreach (var cur in CreatedFiles)
            {
                File.Delete(cur);
            }
        }
        
        private string WriteTemporaryFile(List<string> toWrite)
        {
            string tempFileName = Path.GetTempFileName();
            
            using (StreamWriter streamWriter = new StreamWriter(tempFileName))
            {
                foreach (string cur in toWrite)
                {
                    streamWriter.WriteLine(cur);
                    CreatedFiles.Add(tempFileName);
                }
            }

            return tempFileName;
        }
        
        [DataTestMethod]
        [DataRow("Name: Inigo Montoya", "Inigo", "Montoya")]
        [DataRow("Name: Princess Buttercup", "Princess", "Buttercup")]
        [DataRow("Name: Person McName", "Person", "McName")]
        [DataRow("Name: Person Hyphen-Name", "Person", "Hyphen-Name")]
        public void Import_FirstnameLastname_ProperFormat_Success(string toWrite, string firstName, string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string>(){ toWrite });

            User user = ImportUtility.Import(tempFileName);
            
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }
        
        [DataTestMethod]
        [DataRow("Name: Montoya, Inigo", "Inigo", "Montoya")]
        [DataRow("Name: Buttercup, Princess", "Princess", "Buttercup")]
        [DataRow("Name: McName, Person", "Person", "McName")]
        [DataRow("Name: Hyphen-Name, Person", "Person", "Hyphen-Name")]
        public void Import_LastnameCommaFirstname_ProperFormat_Success(string toWrite, string firstName, string lastName)
        {
            var tempFileName = WriteTemporaryFile(new List<string>(){ toWrite });

            User user = ImportUtility.Import(tempFileName);
            
            Assert.AreEqual(firstName, user.FirstName);
            Assert.AreEqual(lastName, user.LastName);
        }
    }
}