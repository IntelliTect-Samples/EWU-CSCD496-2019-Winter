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
    public class PairingServicesTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(SqliteConnection).Options;

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

        //helper methods
        public User CreateUser()
        {
            var user = new User
            {
                FirstName = "fname",
                LastName = "lname"
            };

            return user;
        }
        public Pairing CreatePairing()
        {
            var pairing = new Pairing
            {
                Recipient = CreateUser(),
                Santa = CreateUser()
            };

            return pairing;
        }

        [TestMethod]
        public void AddNewPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingServices pairingServices = new PairingServices(context);
                Pairing p = CreatePairing();

                Pairing persistedPairing = pairingServices.AddPairing(p);

                Assert.AreEqual(1, persistedPairing.Id);
            }

        }

        [TestMethod]
        public void AddMultiplePairings()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingServices pairingServices = new PairingServices(context);
                Pairing p1 = CreatePairing();
                Pairing p2 = CreatePairing();
                Pairing p3 = CreatePairing();

                Pairing persistedPairing1 = pairingServices.AddPairing(p1);
                Pairing persistedPairing2 = pairingServices.AddPairing(p2);
                Pairing persistedPairing3 = pairingServices.AddPairing(p3);

                Assert.AreEqual(1, persistedPairing1.Id);
                Assert.AreEqual(2, persistedPairing2.Id);
                Assert.AreEqual(3, persistedPairing3.Id);
            }
        }
    }
}
