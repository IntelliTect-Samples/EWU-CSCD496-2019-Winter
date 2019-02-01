using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

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
        public void AddGiftToUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var gift = new DTO.Gift(CreateGiftWithId(1));
            ActionResult<DTO.Gift> result = controller.AddGiftToUser(-1, gift);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastModifiedUserId);
            Assert.IsNull(testService.LastModifiedGift);

        }
        
        [TestMethod]
        public void AddGiftToUser_GiftCannotBeNull()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.AddGiftToUser(1, null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastModifiedUserId);
            Assert.IsNull(testService.LastModifiedGift);
        }
        
        [TestMethod]
        public void AddGiftForUser_ReturnAddedGift()
        {
            var testService = new TestableGiftService { GiftsToReturn = new List<Gift>() };
            var controller = new GiftController(testService);

            var giftBeforeAdd = CreateGiftWithId(1);
            var giftAfterAdd = controller.AddGiftToUser(2, new DTO.Gift(giftBeforeAdd)).Value;

            Assert.AreEqual(2, testService.LastModifiedUserId);
            Assert.AreEqual(giftBeforeAdd.Id, giftAfterAdd.Id);
            Assert.AreEqual(giftBeforeAdd.Title, giftAfterAdd.Title);

            Assert.AreEqual(2, testService.LastModifiedUserId);
        }
        
        [TestMethod]
        public void UpdateGiftForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var gift = new DTO.Gift(CreateGiftWithId(1));
            ActionResult<DTO.Gift> result = controller.UpdateGiftForUser(-1, gift);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastModifiedUserId);
            Assert.IsNull(testService.LastModifiedGift);

        }
        
        [TestMethod]
        public void UpdateGiftForUser_GiftCannotBeNull()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.UpdateGiftForUser(1, null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastModifiedUserId);
            Assert.IsNull(testService.LastModifiedGift);
        }
        
        [TestMethod]
        public void UpdateGiftForUser_ReturnUpdatedGift()
        {
            var testService = new TestableGiftService { GiftsToReturn = new List<Gift>() };
            var controller = new GiftController(testService);

            var giftBeforeUpdate = CreateGiftWithId(1);
            var giftAfterUpdate = controller.UpdateGiftForUser(2, new DTO.Gift(giftBeforeUpdate)).Value;

            Assert.AreEqual(2, testService.LastModifiedUserId);
            Assert.AreEqual(giftBeforeUpdate.Id, giftAfterUpdate.Id);
            Assert.AreEqual(giftBeforeUpdate.Title, giftAfterUpdate.Title);

            Assert.AreEqual(2, testService.LastModifiedUserId);
        }
        
        [TestMethod]
        public void RemoveGiftForUser_GiftCannotBeNull()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<DTO.Gift> result = controller.RemoveGift(null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.IsNull(testService.LastModifiedGift);
        }
        
        [TestMethod]
        public void RemoveGiftForUser_GiftIsRemoved()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            var giftToRemove = CreateGiftWithId(2);
            controller.RemoveGift(new DTO.Gift(giftToRemove));
            var lastGiftRemoved = testService.LastModifiedGift;

            Assert.AreEqual(giftToRemove.Id, lastGiftRemoved.Id);
            Assert.AreEqual(giftToRemove.Title, lastGiftRemoved.Title);
        }

        [TestMethod]
        public void QueryAllGiftsForUser_RequiresPositiveUserId()
        {
            var testService = new TestableGiftService();
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.QueryAllGiftsForUser(-1);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastModifiedUserId);
        }

        [TestMethod]
        public void QueryAllGiftsForUser_ReturnsUsers()
        {
            var gift = CreateGiftWithId(1);
            var testService = new TestableGiftService { GiftsToReturn = new List<Gift> { gift } };
            var controller = new GiftController(testService);

            ActionResult<List<DTO.Gift>> result = controller.QueryAllGiftsForUser(2);

            Assert.AreEqual(2, testService.LastModifiedUserId);
            DTO.Gift resultGift = result.Value.Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
        }

        private Gift CreateGiftWithId(int id)
        {
            return new Gift
            {
                Id = id,
                Title = "gift_" + id,
                Description = "desc_" + id,
                Url = "url_" + id,
                OrderOfImportance = id
            };
        }
    }
}
