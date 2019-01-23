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
        [TestInitialize]
        public void TestInitialize()
        {
            File.Create("test.txt");
        }

        [TestMethod]
        public void BasicFileExistTests()
        {
            Assert.IsTrue(File.Exists("test.txt"));
        }
    }
}
