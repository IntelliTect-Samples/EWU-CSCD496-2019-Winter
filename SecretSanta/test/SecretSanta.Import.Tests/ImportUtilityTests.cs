using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Import.Tests
{
    [TestClass]
    public class ImportUtilityTests
    {
        public List<string> CreatedFiles { get; } = new List<string>();

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
        
        //[TestCleanup]
        
        
        [TestMethod]
        public void Import_WithProperlyFormattedHeader_Success()
        {
            var tempFileName = WriteTemporaryFile("TestStuff");
            Assert.IsNotNull(tempFileName);
        }
    }
}