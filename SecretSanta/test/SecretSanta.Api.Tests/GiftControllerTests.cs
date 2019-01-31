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
        private Gift CreateGift()
        {
            return new Gift
            {
                Id = 3,
                Title = "Gift Tile",
                Description = "Gift Description",
                Url = "http://www.gift.url",
                OrderOfImportance = 1
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GiftController_RequiresGiftService()
        {
            new GiftController(null);
        }

        [TestMethod]
        public void GetGiftForUser_ReturnsUsersFromService()
        {
            var gift = CreateGift();
            var testService = new TestableGiftService
            {
                GetGiftsForUser_Return = new List<Gift>
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
        public void AddGiftForUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            GiftController controller = new GiftController(testService);
            ActionResult<DTO.Gift> result = controller.AddGiftToUser(null, 1);

            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
            Assert.IsNull(testService.AddGiftToUser_Return);
        }

        [TestMethod]
        public void AddGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            GiftController controller = new GiftController(testService);
            ActionResult<DTO.Gift> result = controller.AddGiftToUser(new DTO.Gift(CreateGift()), -1);

            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.AddGiftToUser_UserId);
            Assert.IsNull(testService.AddGiftToUser_Return);
        }

        [TestMethod]
        public void DeleteGiftForUser_RequiresGift()
        {
            var testService = new TestableGiftService();
            GiftController controller = new GiftController(testService);
            ActionResult<DTO.Gift> result = controller.DeleteGiftFromUser(null);
            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.IsNull(testService.RemoveGiftToUser_Gift);
        }



        [TestMethod]
        public void UpdateGiftForUser_RequiresPosId()
        {
            var service = new TestableGiftService();
            var controller = new GiftController(service);


            ActionResult<List<DTO.Gift>> result = controller.UpdateGiftFromUser(-1, new DTO.Gift());

            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GetGiftsForUser_UserId);
            Assert.IsNull(service.UpdateGiftToUser_Gift);
        }
        [TestMethod]
        public void DeleteGiftForUser_ReturnDelete()
        {
            var service = new TestableGiftService();
            var controller = new GiftController(service);
            var gift = CreateGift();
            controller.DeleteGiftFromUser(new DTO.Gift(gift));
            var removedGift = service.RemoveGiftToUser_Gift;

            Assert.AreEqual(removedGift.Description, gift.Description);
            Assert.AreEqual(removedGift.Id, gift.Id);
            Assert.AreEqual(removedGift.OrderOfImportance, gift.OrderOfImportance);
            Assert.AreEqual(removedGift.Title, gift.Title);
            Assert.AreEqual(removedGift.User, gift.User);
            Assert.AreEqual(removedGift.UserId, gift.UserId);
        }
        [TestMethod]
        public void UpdateGiftForUser_RequiresGift()
        {
            var service = new TestableGiftService();
            var controller = new GiftController(service);


            ActionResult<List<DTO.Gift>> result = controller.UpdateGiftFromUser(1, null);

            Assert.IsTrue(result.Result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GetGiftsForUser_UserId);
            Assert.IsNull(service.UpdateGiftToUser_Gift);
        }

        [TestMethod]
        public void UpdateGiftForUser_ReturnUpdate()
        {
            var service = new TestableGiftService();
            var controller = new GiftController(service);
            var gift = CreateGift();
            ActionResult<List<DTO.Gift>> updateGift = controller.UpdateGiftFromUser(2, new DTO.Gift(gift));
            Assert.IsTrue(updateGift.Result is OkObjectResult);
        }
    }
}
