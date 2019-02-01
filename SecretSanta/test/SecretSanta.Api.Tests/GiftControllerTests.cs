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
                ToReturnList =  new List<Gift>
                {
                    gift
                }
            };
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.GetGiftForUser(4);

            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
            Assert.AreEqual(gift.Title, resultGift.Title);
            Assert.AreEqual(gift.Description, resultGift.Description);
            Assert.AreEqual(gift.Url, resultGift.Url);
            Assert.AreEqual(gift.OrderOfImportance, resultGift.OrderOfImportance);
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
        public void AddGiftToUser_ReturnGiftFromService()
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
                ToReturn = gift
            };

            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.AddGift(4, new DTO.Gift(gift));

            Assert.AreEqual(4, testService.AddGiftToUser_UserId);
            Assert.AreEqual(gift.Id, result.Value.Id);
            Assert.AreEqual(gift.Title, result.Value.Title);
            Assert.AreEqual(gift.Description, result.Value.Description);
            Assert.AreEqual(gift.Url, result.Value.Url);
            Assert.AreEqual(gift.OrderOfImportance, result.Value.OrderOfImportance);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresNonNullGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.AddGift(3, null);
            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
            Assert.IsNull(testService.AddGiftToUser_Gift);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresPositiveUserId()
        {
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };

            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.AddGift(-1, new DTO.Gift(gift));
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void UpdateGiftToUser_ReturnGiftFromService()
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
                ToReturn = gift
            };

            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.UpdateGift(4, new DTO.Gift(gift));

            Assert.AreEqual(4, testService.UpdateGiftToUser_UserId);
            Assert.AreEqual(gift.Id, result.Value.Id);
            Assert.AreEqual(gift.Title, result.Value.Title);
            Assert.AreEqual(gift.Description, result.Value.Description);
            Assert.AreEqual(gift.Url, result.Value.Url);
            Assert.AreEqual(gift.OrderOfImportance, result.Value.OrderOfImportance);
        }

        [TestMethod]
        public void UpdateGiftToUser_RequiresNonNullGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.UpdateGift(3, null);
            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
            Assert.IsNull(testService.AddGiftToUser_Gift);
        }

        [TestMethod]
        public void UpdateGiftToUser_RequiresPositiveUserId()
        {
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };

            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.UpdateGift(-1, new DTO.Gift(gift));
            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void RemoveGift_ReturnGiftFromService()
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
                ToReturn = gift
            };

            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.DeleteGift(new DTO.Gift(gift));

            Assert.AreEqual(gift.Id, result.Value.Id);
            Assert.AreEqual(gift.Title, result.Value.Title);
            Assert.AreEqual(gift.Description, result.Value.Description);
            Assert.AreEqual(gift.Url, result.Value.Url);
            Assert.AreEqual(gift.OrderOfImportance, result.Value.OrderOfImportance);
        }

        [TestMethod]
        public void RemoveGift_RequiresNonNullGift()
        {

            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.DeleteGift(null);
            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
            Assert.IsNull(testService.AddGiftToUser_Gift);
        }
    }
}
