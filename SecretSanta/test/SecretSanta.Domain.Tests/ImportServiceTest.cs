using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import;
using System;
using System.IO;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class ImportServiceTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<SecretSantaDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<SecretSantaDbContext>().UseSqlite(SqliteConnection).Options;

            using (var context = new SecretSantaDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void OpenFileForImport_CanRead()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile.txt");
            ImportService importService = new ImportService(path);
            
            Assert.AreEqual<bool>(true, importService.Istream.CanRead);
        }

        [TestMethod]
        public void ReadFileForImport_ReadName()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Name: Brad Howard", importService.ReadName());
        }

        [TestMethod]
        public void ParseName_FirstNameFirst()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        public void ParseName_LastNameFirst()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile2.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingNameAfterColon()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile3.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MalformedInput()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile4.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void PasrseName_MissingLastName()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile5.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingFirstName()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile6.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileForImport_FileNotFound()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile8.txt");
            ImportService importService = new ImportService(path);

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        public void Dispose_Called()
        {
            string path = Path.Combine(System.Environment.CurrentDirectory + @"\..\..\..\", "inputFile.txt");
            ImportService importService;

            using (importService = new ImportService(path))
            {
                Assert.IsNotNull(importService.Istream);
            }

            Assert.IsNull(importService.Istream);
        }
    }
}