using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile.txt");
            
            Assert.AreEqual<bool>(true, importService.Istream.CanRead);
        }

        [TestMethod]
        public void ReadFileForImport_ReadName()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile.txt");

            Assert.AreEqual<string>("Name: Brad Howard", importService.ReadName());
        }

        [TestMethod]
        public void ParseName_FirstNameFirst()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile.txt");

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        public void ParseName_LastNameFirst()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile2.txt");

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingNameAfterColon()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile3.txt");

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseName_MissingFirstName()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile7.txt");

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFileForImport_FileNotFound()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\inputFile8.txt");

            Assert.AreEqual<string>("Brad", importService.ParseName(importService.ReadName())[0]);
        }
    }
}