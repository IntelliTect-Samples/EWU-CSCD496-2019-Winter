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
            Gift gift = new Gift
            {
                Title = "Nintendo Switch",
                OrderOfImportance = 1,
                Description = "Nintendo's Premier Gaming Console.",
                Url = "nintendo.com"
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
        public void AddGiftToUser()
        {
            GiftService giftService;
            UserService userService;
            Gift gift = CreateGift();
            User user = new User
            {
                FirstName = "Conner",
                LastName = "Verret",
                Gifts = new List<Gift>()
            };

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                userService = new UserService(context);

                user = userService.CreateUser(user);
                giftService.AddGiftToUser(gift, user.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                user = userService.Find(1);
            }
            Assert.AreEqual("Nintendo Switch", user.Gifts[0].Title);
        }

        [TestMethod]
        public void DeleteGiftFromUser()
        {
            GiftService giftService;
            UserService userService;
            Gift gift = CreateGift();
            User user = new User
            {
                FirstName = "Conner",
                LastName = "Verret",
                Gifts = new List<Gift>()
            };

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                userService = new UserService(context);

                user = userService.CreateUser(user);
                giftService.AddGiftToUser(gift, user.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                userService = new UserService(context);

                giftService.DeleteGiftFromUser(gift, user.Id);
                user = userService.Find(1);
            }
            Assert.IsTrue(user.Gifts.Count == 0);
        }

        [TestMethod]
        public void UpdateGiftFromUser()
        {
            GiftService giftService;
            UserService userService;
            Gift gift = CreateGift();
            User user = new User
            {
                FirstName = "Conner",
                LastName = "Verret",
                Gifts = new List<Gift>()
            };

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                userService = new UserService(context);

                user = userService.CreateUser(user);
                giftService.AddGiftToUser(gift, user.Id);
            }

            gift.Title = "Xbox";

            using (var context = new ApplicationDbContext(Options))
            {
                giftService = new GiftService(context);
                userService = new UserService(context);

                giftService.UpdateGiftForUser(gift, user.Id);
                user = userService.Find(1);
            }
            Assert.AreEqual("Xbox", user.Gifts[0].Title);
        }
    }
}
