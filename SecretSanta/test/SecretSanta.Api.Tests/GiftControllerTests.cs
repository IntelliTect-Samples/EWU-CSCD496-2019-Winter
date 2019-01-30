using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using SecretSanta.Api.DTO;
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
        public void CreateGift()
        {
            var gift = GiftControllerTests.GetGift();

            var testService = new TestableGiftService();
            testService.CreateGift(gift);

            var controller = new GiftController(testService);

            ActionResult result = controller.CreateGift(new DTO.Gift(gift));

            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(gift.Title, testService.CreateGift_Return.Title);
        }

        [TestMethod]
        public void UpdateGift()
        {
            var gift = GiftControllerTests.GetGift();
            var testService = new TestableGiftService();

            var controller = new GiftController(testService);

            ActionResult result = controller.CreateGift(new DTO.Gift(gift));

            gift.Title = "The Alchemist";
            testService.UpdateGift(gift);

            result = controller.UpdateGift(new DTO.Gift(gift));
            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(gift.Title, testService.UpdateGift_Return.Title);
        }

        [TestMethod]
        public void DeleteGift()
        {
            var gift = GiftControllerTests.GetGift();
            var testService = new TestableGiftService();

            var controller = new GiftController(testService);

            ActionResult result = controller.CreateGift(new DTO.Gift(gift));

            testService.DeleteGift(gift);
            result = controller.DeleteGift(new DTO.Gift(gift));

            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(gift.Title, testService.CreateGift_Return.Title);
        }

        [TestMethod]
        public void UpdateGiftForUser()
        {
            var gift = GiftControllerTests.GetGift();

            var testService = new TestableGiftService();
            testService.UpdateGiftForUser(gift, 4);

            var controller = new GiftController(testService);

            ActionResult result = controller.UpdateGiftForUser(new DTO.Gift(gift), 4);
            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(4, testService.UpdateGiftForUser_UserId);
        }

        [TestMethod]
        public void DeleteGiftFromUser() 
        {
            var gift = GiftControllerTests.GetGift();
            var testService = new TestableGiftService();
            testService.DeleteGiftFromUser(gift, 4);

            var controller = new GiftController(testService);

            ActionResult result = controller.DeleteGiftFromUser(new DTO.Gift(gift), 4);
            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(gift.Title, testService.DeleteGiftFromUser_Gift.Title);
            Assert.AreEqual(4, testService.DeleteGiftFromUser_UserId);
        }

        [TestMethod]
        public void GetGiftForUser_ReturnsUsersFromService()
        {
            var gift = GiftControllerTests.GetGift();

            var returnList = new List<Domain.Models.Gift>();
            returnList.Add(gift);

            var testService = new TestableGiftService();

            testService.GetGiftsForUser_Return = returnList;

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
        public void AddGiftToUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult result = controller.AddGiftToUser(null, 4);
            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void AddGiftToUser_ReturnsUserFromService()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);
            Domain.Models.Gift gift = new Domain.Models.Gift
            {
                Id = 42
            };

            ActionResult result = controller.AddGiftToUser(new DTO.Gift(gift), 3);

            OkResult okResult = result as OkResult;

            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(3, testService.AddGiftToUser_UserId);
            Assert.AreEqual(gift.Title, testService.AddGiftToUser_Gift.Title);
        }

        private static Domain.Models.Gift GetGift()
        {
            return new Domain.Models.Gift
            {
                Id = 3,
                Title = "Gift Title",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };
        }
    }
}
