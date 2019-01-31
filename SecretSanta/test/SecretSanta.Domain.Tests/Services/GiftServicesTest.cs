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
    public class GiftServicesTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        public Gift CreateGift()
        {
            User user = new User
            {
                FirstName = "Conner",
                LastName = "Verret",
                Gifts = new List<Gift>()
            };

            Gift gift = new Gift
            {
                Title = "Nintendo Switch",
                OrderOfImportance = 1,
                Description = "Nintendo's Premier Gaming Console.",
                Url = "nintendo.com",
                User = user,
            };

            return gift;
        }

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

        [TestMethod]
        public void AddGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                Gift gift = CreateGift();
                Gift userGift = giftService.CreateGift(gift);
                Assert.AreNotEqual(0, userGift.Id);
            }
        }

        [TestMethod]
        public void FindGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                giftService.CreateGift(gift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                gift = giftService.Find(1);

                Assert.AreEqual("Nintendo Switch", gift.Title);
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                giftService.CreateGift(gift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);

                gift = giftService.Find(1);

                gift.Title = "Xbox One";
                giftService.UpdateGift(gift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                gift = giftService.Find(1);

                Assert.AreEqual("Xbox One", gift.Title);
            }
        }
        [TestMethod]
        public void DeleteGift()
        {
            GiftService giftService;
            Gift gift = CreateGift();

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                giftService.CreateGift(gift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                giftService.DeleteGift(gift);
                Assert.IsNull(giftService.Find(gift.Id));
            }
        }
    }
}
