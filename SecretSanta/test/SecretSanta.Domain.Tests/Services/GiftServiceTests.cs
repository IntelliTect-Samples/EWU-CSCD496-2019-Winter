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
    public class GiftServiceTests
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

        private Gift CreateGift()
        {
            return new Gift {
                Title = "My Gift",
                OrderOfImportance = 2,
                Url = "shop.com",
                Description = "A fancy gift",
                User = new User()
            };
        }

        [TestMethod]
        public void AddGift()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                Gift gift = CreateGift();

                service.AddGift(gift);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                var fetchedGift = service.Find(1);

                Assert.AreEqual("My Gift", fetchedGift.Title);
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                Gift gift = CreateGift();

                service.AddGift(gift);

                gift.Title = "A very fancy gift";
                service.UpdateGift(gift);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                var fetchedGift = service.Find(1);

                Assert.AreEqual("A very fancy gift", fetchedGift.Title);
            }
        }

        [TestMethod]
        public void RemoveGift()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                Gift gift = CreateGift();

                service.AddGift(gift);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(dbContext);
                var fetchedGift = service.Find(1);

                service.RemoveGift(fetchedGift);

                Assert.IsNull(service.Find(fetchedGift.Id));
            }
        }
    }
}
