using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Import;
using System;
using System.Collections.Generic;
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
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\imputFile.txt");
            
            Assert.AreEqual<bool>(true, importService.Istream.CanRead);
        }

        [TestMethod]
        public void ReadFileForImport_ReadName()
        {
            ImportService importService = new ImportService(@"D:\CScD Class\CScD 496\Repo\EWU-CSCD496-2019-Winter\SecretSanta\src\SecretSanta.Domain\imputFile.txt");

            Assert.AreEqual<string>("Name: Brad Howard", importService.ReadName());
        }
    }
}
