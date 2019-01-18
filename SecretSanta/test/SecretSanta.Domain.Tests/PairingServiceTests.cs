using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class PairingServiceTests
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
        public void CreatePairing()
        {
            PairingService pairingService;
            UserService userService;

            User santa = new User() { First = "Rena", Last = "Hau" };
            User reciver = new User() { First = "Fox", Last = "Hau" };

            using (var context = new SecretSantaDbContext(Options))
            {
                pairingService = new PairingService(context);
                userService = new UserService(context);

                Pairing pair = new Pairing { Santa = santa, UserFor = reciver };

                userService.UpsertUser(santa);
                userService.UpsertUser(reciver);

                pair.SantaId = santa.Id;
                pair.UserForId = reciver.Id;

                pairingService.CreatePairing(pair);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                pairingService = new PairingService(context);

                Assert.AreEqual<int>(1, pairingService.FindPairing(1).SantaId);
            }
        }
    }
}
