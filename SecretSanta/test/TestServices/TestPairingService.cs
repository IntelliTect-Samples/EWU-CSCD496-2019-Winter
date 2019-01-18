using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;

namespace test.TestServices
{
    [TestClass]
    public class TestPairingService
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        private Pairing Pair { get; set; }

        private static Pairing CreatePairing()
        {
            var santa = new User
            {
                FirstName = "King",
                LastName = "Arthur"
            };

            var recep = new User
            {
                FirstName = "Black",
                LastName = "Knight"
            };

            var pair = new Pairing
            {
                Santa = santa,
                Recepiant = recep
            };

            return pair;
        }


        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
            Pair = CreatePairing();
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                Pairing pairing = CreatePairing();
                pairingService.Add(pairing);
                Assert.AreNotEqual(0, pairing.Id);
            }
        }

        [TestMethod]
        public void UpdatePairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairService = new PairingService(context);
                pairService.Add(Pair);
                Assert.IsNotNull(pairService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairService = new PairingService(context);
                Pair = pairService.Find(1);
                Pair.Santa = new User
                {
                    FirstName = "White",
                    LastName = "Knight"
                };
                pairService.Update(Pair);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairService = new PairingService(context);
                Pairing tempPair = pairService.Find(1);
                Assert.AreEqual("White", tempPair.Santa.FirstName);
            }
        }

        [TestMethod]
        public void FindPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                Assert.IsNull(pairingService.Find(1));
                pairingService.Add(Pair);
                Assert.IsNotNull(pairingService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                Pairing foundPair = pairingService.Find(1);
                Assert.IsNotNull(foundPair);
            }
        }

        [TestMethod]
        public void IsPairNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                bool isPairNull = pairingService.IsPairingNull(Pair);
                Assert.IsFalse(isPairNull);
            }
        }

        [TestMethod]
        public void IsPairNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context);
                Pair = null;
                bool isPairNull = pairingService.IsPairingNull(Pair);
                Assert.IsTrue(isPairNull);
            }
        }

    }
}
