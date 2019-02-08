using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        public void GetGiftForUser_ReturnsGiftsFromService()
        {
            var user = new User
            {
                Id = 4
            };

            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                User = user,
                UserId = user.Id
            };

            var viewModel = new GiftInputViewModel
            {
                Title = gift.Title,
                Description = gift.Description,
                Url = gift.Url
            };

            var testService = new TestableGiftService
            {
                ToReturn = new List<Gift>
                {
                    gift
                }
            };

            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.GetGiftForUser(user.Id);

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            Assert.AreEqual(viewModel.Title, testService.ToReturn[0].Title);
            Assert.AreEqual(viewModel.Description, testService.ToReturn[0].Description);
        }

        [TestMethod]
        public void GetGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.GetGiftForUser(-1);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void AddGiftToUser_ReturnsGiftFromService()
        {
            var user = new User
            {
                Id = 4
            };

            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                User = user,
                UserId = user.Id
            };

            var viewModel = new GiftInputViewModel
            {
                Title = gift.Title,
                Description = gift.Description,
                Url = gift.Url
            };

            var testService = new TestableGiftService();

            testService.AddGiftToUser_UserId = user.Id;
            testService.Return = gift;

            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.AddGiftToUser(1, viewModel);

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(gift.Title, testService.Return.Title);
            Assert.AreEqual(gift.Description, testService.Return.Description);
        }

        [TestMethod]
        public void AddGiftToUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.GetGiftForUser(-1);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
        }

        [TestMethod]
        public void UpdateGiftToUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.UpdateGiftForUser(-1, null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.UpdateGift_UserId);
        }

        [TestMethod]
        public void UpdateGiftToUser_ReturnsGiftFromService()
        {
            var user = new User
            {
                Id = 4
            };

            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                User = user,
                UserId = user.Id
            };

            var viewModel = new GiftInputViewModel
            {
                Title = gift.Title,
                Description = gift.Description,
                Url = gift.Url
            };

            var testService = new TestableGiftService();

            testService.UpdateGift_UserId = user.Id;
            testService.Return = gift;

            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.UpdateGiftForUser(1, viewModel);

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(gift.Title, testService.Return.Title);
            Assert.AreEqual(gift.Description, testService.Return.Description);
        }

        [TestMethod]
        public void RemoveGiftFromUser_ReturnsTrue()
        { 
            var gift = new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url"
            };

            var testService = new TestableGiftService();

            testService.RemoveGift_GiftId = gift.Id;
            testService.RemoveReturn = true;
            bool removed = testService.RemoveGift(3);

            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.Delete(3);

            Assert.IsTrue(result is OkResult);
            Assert.IsTrue(testService.RemoveReturn);
        }
    }
}
