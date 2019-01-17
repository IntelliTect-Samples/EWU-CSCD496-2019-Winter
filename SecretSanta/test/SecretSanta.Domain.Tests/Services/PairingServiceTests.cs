using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        private Pairing CreatePairing()
        {
            User recipient = new User { FirstName = "Recipient" };
            User santa = new User { FirstName = "Santa" };
            Group group = new Group();

            return new Pairing { Recipient = recipient, Santa = santa, Group = group};
        }

        [TestMethod]
        public void AddPairing()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                PairingService service = new PairingService(dbContext);
                Pairing pairing = CreatePairing();

                service.AddPairing(pairing);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                PairingService service = new PairingService(dbContext);
                var fetchedPairing = service.Find(1);

                Assert.AreEqual("Recipient", fetchedPairing.Recipient.FirstName);
                Assert.AreEqual("Santa", fetchedPairing.Santa.FirstName);
            }
        }
    }
}
