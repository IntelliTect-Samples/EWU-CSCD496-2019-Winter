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
    public class PairingServicesTest
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

            User santa = new User
            {
                FirstName = "Alan",
                LastName = "Watts"
            };

            User recipient = new User
            {
                FirstName = "Rene",
                LastName = "Descartes"
            };

            Pairing pairing = new Pairing
            {
                Santa = santa,
                Recipient = recipient
            };

            return pairing;
        }

        [TestMethod]
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                Pairing pairing = CreatePairing();
                Pairing addedPairing = pairingService.UpsertPairing(pairing);
                Assert.AreNotEqual(0, addedPairing.Id);
            }
        }

        [TestMethod]
        public void FindPairing()
        {
            PairingService pairingService;
            Pairing pairing = CreatePairing();

            using (var context = new ApplicationDbContext(Options))
            {
                pairingService = new PairingService(context);
                pairingService.UpsertPairing(pairing);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                pairingService = new PairingService(context);
                pairing = pairingService.Find(1);
                Assert.AreEqual("Alan", pairing.Santa.FirstName);
            }
        }

    }
}
