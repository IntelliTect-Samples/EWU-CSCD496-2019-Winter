using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace test.TestServices
{
    [TestClass]
    public class TestGiftsService
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        private GiftsService GiftService { get; set; }
        private Gift Gift { get; set; }

        private static Gift CreateGift()
        {
            var user = new User
            {
                FirstName = "Ash",
                LastName = "Ketchum"
            };

            var gift = new Gift
            {
                Title = "Bulbasaur",
                Description = "Best Grass Type Starter Pokemon.",
                OrderOfImportance = 1,
                Url = "www.pokemonleague.com",
                User = user
            };

            user.GiftList = new List<Gift> { gift };
            return gift;
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
            Gift = CreateGift();
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
                GiftService = new GiftsService(context);
                GiftService.AddGiftToDb(Gift);
                Assert.AreEqual(1, Gift.Id);
            }
        }

        [TestMethod]
        public void UpdateGiftAfterAdd()
        {
            Gift = CreateGift();

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                GiftService.AddGiftToDb(Gift);
                Assert.IsNotNull(GiftService.FindGift(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift = GiftService.FindGift(1);
                Gift.Title = "Charmander";
                GiftService.UpdateGift(Gift);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift = GiftService.FindGift(1);
                Assert.AreEqual("Charmander", Gift.Title);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateGiftThatIsNotThere()
        {
            Gift = CreateGift();
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift = GiftService.FindGift(1);
                Assert.IsNull(Gift);
                Gift.Title = "Charmander";
                GiftService.UpdateGift(Gift);
            }
        }

        [TestMethod]
        public void FindGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Assert.IsNull(GiftService.FindGift(1));
                GiftService.AddGiftToDb(Gift);
                Assert.IsNotNull(GiftService.FindGift(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift foundGift = GiftService.FindGift(1);
                Assert.IsNotNull(foundGift);
            }
        }

        [TestMethod]
        public void NotFindGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift = GiftService.FindGift(1);
                Assert.IsNull(Gift);
            }
        }

        [TestMethod]
        public void DeleteGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Assert.IsNull(GiftService.FindGift(1));
                GiftService.AddGiftToDb(Gift);
                Assert.IsNotNull(GiftService.FindGift(1));
            }
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                GiftService.DeleteGift(Gift);
                Assert.IsNull(GiftService.FindGift(1));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotDeleteGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Assert.IsNull(GiftService.FindGift(1));
                GiftService.DeleteGift(Gift);
            }
        }

        [TestMethod]
        public void IsGiftNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                bool isGiftNull = GiftService.IsGiftNull(Gift);
                Assert.IsFalse(isGiftNull);
            }
        }

        [TestMethod]
        public void IsGiftNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService = new GiftsService(context);
                Gift = null;
                bool isGiftNull = GiftService.IsGiftNull(Gift);
                Assert.IsTrue(isGiftNull);
            }
        }

    }
}