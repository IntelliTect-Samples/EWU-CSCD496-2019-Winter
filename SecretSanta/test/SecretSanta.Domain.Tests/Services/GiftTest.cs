using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GiftTest : ApplicationServiceTest
    {
        Gift CreateGift()
        {
            var gift = new Gift
            {
                Title = "The Princess Bride",
                URL = "www.website.com",
                Description = "magic and love and stuff",
                OrderOfImportance = 1,
                User = new User { FirstName = "Inigo", LastName = "Montoya" }
            };
            return gift;
        }

        [TestMethod]
        public void AddGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(context);
                var myGift = CreateGift();

                var persistedGift = service.AddGift(myGift);
                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }

        [TestMethod]
        public void UpdateGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(context);
                var myGift = CreateGift();
                var persistedGift = service.AddGift(myGift);

                persistedGift.Title = "asdf";
                service.UpdateGift(persistedGift);

                Assert.AreEqual(myGift.Id, persistedGift.Id);
                Assert.AreNotEqual("The Princess Bride", persistedGift.Title);
            }
        }

        [TestMethod]
        public void DeleteGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService service = new GiftService(context);
                var myGift = CreateGift();
                var persistedGift = service.AddGift(myGift);

                int id = service.RemoveGift(persistedGift);
                var retrievedGift = service.FindById(id);

                Assert.AreEqual(null, retrievedGift);
            }
        }

    }
}
