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
            Gift gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                URL = "http://www.gift.url",
                WantTier = 1
            };
            TestableGiftService service = new TestableGiftService
            {
                ToReturn =  new List<Gift>
                {
                    gift
                }
            };
            GiftController controller = new GiftController(service);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(4);

            Assert.AreEqual(4, service.GetGiftsForUser_UserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
            Assert.AreEqual(gift.Title, resultGift.Title);
            Assert.AreEqual(gift.Description, resultGift.Description);
            Assert.AreEqual(gift.URL, resultGift.Url);
            Assert.AreEqual(gift.WantTier, resultGift.OrderOfImportance);
        }

        [TestMethod]
        public void GetGiftForUser_ReturnsNotFound()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(-1);
            
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void MakeGiftForUser_ReturnBadRequest()
        {
            TestableGiftService testableService = new TestableGiftService();
            GiftController giftController = new GiftController(testableService);

            ActionResult result = giftController.MakeGift(4, null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testableService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void MakeGiftForUser_ReturnOk()
        {
            TestableGiftService testableService = new TestableGiftService();
            GiftController giftController = new GiftController(testableService);

            ActionResult result = giftController.MakeGift(4, new DTO.Gift());

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void UpdateGiftForUser_ReturnNotFound()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult result = controller.UpdateGiftForUser(-1, new DTO.Gift());

            Assert.IsTrue(result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void UpdateGiftForUser_ReturnBadRequest()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult result = controller.UpdateGiftForUser(1, null);

            Assert.IsTrue(result is BadRequestObjectResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void UpdateGiftForUser_ReturnOk()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult result = controller.UpdateGiftForUser(1, new DTO.Gift());

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void DeleteGiftFromUser_ReturnNotFound()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult result = controller.DeleteGift(-1, 1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void DeleteGiftFromUser_ReturnOk()
        {
            TestableGiftService service = new TestableGiftService();
            GiftController controller = new GiftController(service);

            ActionResult result = controller.DeleteGift(1, 1);

            Assert.IsTrue(result is OkResult);
        }
    }
}
