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
        private string Path = "test.txt";

        [TestInitialize]
        public void TestInitialize()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            
        }

        [TestMethod]
        public void BasicFileExistTest()
        {
            using (FileStream fs = File.Create(Path))
            {}
            Assert.IsTrue(File.Exists("test.txt"));
        }

        [TestMethod]
        public void TestToSeeIfThereIsTextInTheFile()
        {
            using (FileStream fs = File.Create(Path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("text in a file");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            using (StreamReader sr = File.OpenText(Path))
            {
                string s = sr.ReadLine();
                Assert.AreEqual("text in a file", s);
            }
        }



        
    }

}
