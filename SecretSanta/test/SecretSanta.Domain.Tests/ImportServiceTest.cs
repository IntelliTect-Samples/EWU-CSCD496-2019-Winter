using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Import;
using System;
using System.IO;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class ImportServiceTest
    {
        [TestInitialize]
        public void MakeTestFiles()
        {
            BuildTestFile("Name: Brad Howard", "inputFile.txt");
            BuildTestFile("Name: Howard, Brad", "inputFile2.txt");
            BuildTestFile("Name:", "inputFile3.txt");
            BuildTestFile("Name", "inputFile4.txt");
            BuildTestFile("Name: Howard", "inputFile5.txt");
            BuildTestFile("Name: Howard,", "inputFile6.txt");
            BuildTestFile("Name:Howard,", "inputFile7.txt");
        }

        [TestCleanup]
        public void CleanupFiles()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile2.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile3.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile4.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile5.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile6.txt"));
            File.Delete(Path.Combine(Path.GetTempPath(), "inputFile7.txt"));
        }

        public void BuildTestFile(string input, string fileName)
        {
            string path = Path.Combine(Path.GetTempPath(), fileName);

            if(!File.Exists(path))
            {
                using (StreamWriter ostream = File.CreateText(path))
                {
                    ostream.WriteLine(input);
                    ostream.Close();
                }
            }
        }

        [TestMethod]
        public void OpenFileForImport_CanRead()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path);
            
            Assert.AreEqual<bool>(true, importService.Istream.CanRead);
        }

        [TestMethod]
        public void ReadFileForImport_ReadName()
        {
            string path = Path.Combine(System.IO.Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Name: Brad Howard", importService.ReadLine());

            importService.Dispose();
        }

        [TestMethod]
        public void ParseName_FirstNameFirst()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        public void ParseName_LastNameFirst()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile2.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingNameAfterColon()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile3.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MalformedInput()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile4.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MissingLastName()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile5.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingFirstName()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile6.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileForImport_FileNotFound()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile8.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadLine())[0]);

            importService.Dispose();
        }

        [TestMethod]
        public void Dispose_Called()
        {
            string path = Path.Combine(Path.GetTempPath(), "inputFile.txt");
            ImportService importService;

            using (importService = new ImportService(path))
            {
                Assert.IsNotNull(importService.Istream);
            }

            Assert.IsNull(importService.Istream);
        }
    }
}