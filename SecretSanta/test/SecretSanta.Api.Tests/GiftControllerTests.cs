using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            new GiftController(null);
        }

        [TestMethod]
        public void GetGiftForUser_ReturnsUsersFromService()
        {
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };
            var testService = new TestableGiftService
            {
                ToReturn =  new List<Gift>
                {
                    gift
                }
            };
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(4);

            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual(gift.Id, resultGift.GiftId);
            Assert.AreEqual(gift.Title, resultGift.GiftTitle);
            Assert.AreEqual(gift.Description, resultGift.GiftDescription);
            Assert.AreEqual(gift.Url, resultGift.GiftUrl);
            Assert.AreEqual(gift.OrderOfImportance, resultGift.GiftOrderOfImportance);
        }

        [TestMethod]
        public void GetGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(-1);
            
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_UserIDInvalid()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.AddGiftToUser(-1,new DTO.Gift());

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void AddGiftToUser_NullGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.AddGiftToUser(1, null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void RemoveGift_NullGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.RemoveGift(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateGiftForUser_InvalidID()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.UpdateGiftForUser(-1,null);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void UpdateGiftForUser_NullGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.UpdateGiftForUser(1, null);

            Assert.IsTrue(result is BadRequestResult);
        }
    }
}
