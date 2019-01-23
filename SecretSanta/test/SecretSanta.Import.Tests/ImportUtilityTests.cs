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
        
        private string WriteTemporaryFile(string toWrite)
        {
            string tempFileName = Path.GetTempFileName();
            
            File.WriteAllText(tempFileName, toWrite);
            CreatedFiles.Add(tempFileName);

            return tempFileName;
        }
        
        [TestMethod]
        public void Import_WithProperlyFormattedHeader_Success()
        {
            var tempFileName = WriteTemporaryFile("Inigo Montoya");

            User user = ImportUtility.Import(tempFileName);
            
            Assert.AreEqual("Inigo", user.FirstName);
            Assert.AreEqual("Montoya", user.LastName);
        }
    }
}