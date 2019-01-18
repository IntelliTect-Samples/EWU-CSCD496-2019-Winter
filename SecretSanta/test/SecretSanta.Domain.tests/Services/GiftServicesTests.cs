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
    public class GiftServicesTests
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
        public Gift CreateGift()
        {
            var gift = new Gift()
            {
                Title = "Toy",
                OrderOfImportance = 1,
                Url = "not/a/real.url",
                description = "description",
                User = CreateUser()
            };

            return gift;
        }

        [TestMethod]
        public void AddNewGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftServices giftServices = new GiftServices(context);
                Gift g = CreateGift();

                Gift persistantGift = giftServices.AddUpdateGift(g);
                Assert.AreEqual(1, persistantGift.Id);
            }
        }

        [TestMethod]
        public void AddMultipleNewGifts()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftServices giftServices = new GiftServices(context);
                Gift g1 = CreateGift();
                Gift g2 = CreateGift();
                Gift g3 = CreateGift();

                Gift persistantGift1 = giftServices.AddUpdateGift(g1);
                Gift persistantGift2 = giftServices.AddUpdateGift(g2);
                Gift persistantGift3 = giftServices.AddUpdateGift(g3);

                Assert.AreEqual(1, persistantGift1.Id);
                Assert.AreEqual(2, persistantGift2.Id);
                Assert.AreEqual(3, persistantGift3.Id);
            }
        }

        [TestMethod]
        public void AddNewGiftThenUpdate()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftServices giftServices = new GiftServices(context);
                Gift g1 = CreateGift();

                Gift persistantGift1 = giftServices.AddUpdateGift(g1);

                g1.OrderOfImportance = 4;

                Gift persistantGift2 = giftServices.AddUpdateGift(g1);

                Assert.AreEqual(persistantGift1.Id, persistantGift2.Id);

            }
        }

        [TestMethod]
        public void AddNewGiftThenRemoveGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftServices giftServices = new GiftServices(context);
                Gift g1 = CreateGift();

                giftServices.AddUpdateGift(g1);
                int removedIndex = giftServices.RemoveGift(g1);

                
                Assert.AreEqual(1, removedIndex);

            }
        }
    }
}
