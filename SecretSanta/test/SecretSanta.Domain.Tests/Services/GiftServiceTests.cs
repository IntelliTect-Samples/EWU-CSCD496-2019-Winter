using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

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

        private static Gift CreateGift(string fName = "Inigo", string lName = "Montoya", string giftName = "Roomba")
        {
            var user = new User
            {
                FirstName = fName,
                LastName = lName
            };

            var gift = new Gift
            {
                Title = giftName,
                OrderOfImportance = 1,
                Url = "www.someUrl.com",
                Description = "Never vacuum again",
                User = user
            };

            return gift;
        }

        private static List<Gift> CreateGifts(int numberToCreate = 2)
        {
            var gifts = new List<Gift>();

            for (var i = 0; i < numberToCreate; i++)
                gifts.Add(CreateGift($"firstName{i}", $"lastName{i}", $"giftName{i}"));

            return gifts;
        }

        [TestMethod]
        public void UpsertGift_TestWithStaticGift_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var myGift = CreateGift();

                var addedGift = service.UpsertGift(myGift);

                Assert.AreEqual(1, addedGift.Id);
            }
        }

        [TestMethod]
        public void UpsertGift_TestUpdateGift_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var myGift = CreateGift();

                // Add gift
                service.UpsertGift(myGift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var retrievedGift = service.Find(1);

                // Update gift
                retrievedGift.Title = "Echo Show";
                service.UpsertGift(retrievedGift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var retrievedGift = service.Find(1);

                // Validate change
                Assert.AreEqual("Echo Show", retrievedGift.Title);
            }
        }

        [TestMethod]
        public void DeleteGift_TestRemoveStaticGift_Success()
        {
            var myGift = CreateGift();

            // Add gift into DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);

                var addedGift = service.UpsertGift(myGift);

                Assert.AreEqual(1, addedGift.Id);
            }

            // Remove gift from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);

                service.DeleteGift(myGift);

                Assert.IsNull(service.Find(1));
            }
        }

        [TestMethod]
        public void FindGift_TestFindStaticGift_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var myGift = CreateGift();

                var addedGift = service.UpsertGift(myGift);

                Assert.AreEqual(1, addedGift.Id);
            }

            // Remove gift from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var myGift = CreateGift();

                var foundGift = service.Find(1);

                Assert.AreEqual(1, foundGift.Id);
                Assert.AreEqual(1, foundGift.User.Id);
            }
        }

        [TestMethod]
        public void FetchGifts_TestWithStaticGifts_Success()
        {
            // arrange
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var gifts = CreateGifts();

                foreach (var cur in gifts) service.UpsertGift(cur);
            }

            // act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new GiftService(context);
                var fetchedGifts = service.FetchAll();

                // assert
                for (var i = 0; i < fetchedGifts.Count; i++) Assert.AreEqual(i + 1, fetchedGifts[i].Id);
            }
        }
    }
}