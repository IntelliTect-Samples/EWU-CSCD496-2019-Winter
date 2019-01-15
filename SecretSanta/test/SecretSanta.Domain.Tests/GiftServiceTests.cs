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
    public class GiftServiceTests
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
        public void CreateGift()
        {
            GiftService giftService;

            User user = new User() { First = "Brad", Last = "Howard" };

            Gift gift = new Gift()
            {
                Title = "Toaster Oven",
                Description = "Toaster",
                URL = "toasters.co.su",
                WantTier = 999,
                WhoWantIt = user
            };

            using (var context = new SecretSantaDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.CreateGift(user, gift);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                giftService = new GiftService(context);

                Assert.AreEqual(true, giftService.HasGift(user, gift));
            }
        }
    }
}
