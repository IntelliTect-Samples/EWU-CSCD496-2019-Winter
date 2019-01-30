using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            var giftController = new GiftController(null);
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
            var returnList = new List<Gift> {gift};

            var testService = new TestableGiftService {GetGiftsForUser_Return = returnList};

            var controller = new GiftController(testService);

            var result = controller.GetGiftForUser(4);

            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            var resultGift = result.Value.Single();
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

            var result = controller.GetGiftForUser(-1);

            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var result = controller.AddGiftToUser(null, 4);

            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the controller does not AddGitToUser on the service
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_InvokesService()
        {
            var giftDto = new DTO.Gift
            {
                Id = 42,
                Title = "Title",
                Description = "Description",
                OrderOfImportance = 1,
                Url = "Url"
            };
            var testService = new TestableGiftService {AddGiftToUser_Return = DTO.Gift.ToEntity(giftDto)};
            var controller = new GiftController(testService);

            var result = controller.AddGiftToUser(giftDto, 4);

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(4, testService.AddGiftToUser_UserId);

            // Ensure service was called
            Assert.AreEqual(giftDto.Id, testService.AddGiftToUser_Gift.Id);
        }

        [TestMethod]
        public void UpdateGiftForUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var result = controller.UpdateGiftForUser(null, 4);

            Assert.IsTrue(result.Result is BadRequestResult);

            // Ensure GiftService was not called
            Assert.AreEqual(0, testService.UpdateGiftForUser_userId);
        }

        [TestMethod]
        public void UpdateGiftForUser_RequiresPositiveUserId()
        {
            var giftDto = new DTO.Gift
            {
                Id = 42,
                Title = "Title",
                Description = "Description",
                OrderOfImportance = 1,
                Url = "Url"
            };

            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var result = controller.UpdateGiftForUser(giftDto, -1);

            Assert.IsTrue(result.Result is NotFoundObjectResult, $"{result.Result} is not instance of NotFoundResult");

            // Ensure GiftService was not called
            Assert.AreEqual(0, testService.UpdateGiftForUser_userId);
        }

        [TestMethod]
        public void UpdateGiftForUser_InvokesService()
        {
            var giftDto = new DTO.Gift
            {
                Id = 42,
                Title = "Title",
                Description = "Description",
                OrderOfImportance = 1,
                Url = "Url"
            };
            var testService = new TestableGiftService {UpdateGiftForUser_Return = DTO.Gift.ToEntity(giftDto)};
            var controller = new GiftController(testService);

            var result = controller.UpdateGiftForUser(giftDto, 4);

            Assert.IsNotNull(result, "Result was not a 200");
            Assert.AreEqual(4, testService.UpdateGiftForUser_userId);

            // Ensure service was called
            Assert.AreEqual(giftDto.Id, testService.UpdateGiftForUser_Gift.Id);
        }

        [TestMethod]
        public void RemoveGiftFromUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var result = controller.UpdateGiftForUser(null, 4);

            Assert.IsTrue(result.Result is BadRequestResult);

            // Ensure GiftService was not called
            Assert.AreEqual(0, testService.UpdateGiftForUser_userId);
        }

        [TestMethod]
        public void RemoveGiftFromUser_InvokesService()
        {
            var giftDto = new DTO.Gift
            {
                Id = 42,
                Title = "Title",
                Description = "Description",
                OrderOfImportance = 1,
                Url = "Url"
            };
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.RemoveGiftFromUser(giftDto);

            Assert.IsNotNull(result, "Result was not a 200");

            // Ensure service was called
            Assert.AreEqual(giftDto.Id, testService.RemoveGift_Gift.Id);
        }
    }
}