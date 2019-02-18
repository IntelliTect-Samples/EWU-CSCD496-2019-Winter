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
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
        [AssemblyInitialize]
        public static void ConfigureAutoMapper(TestContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
        }

        [TestMethod]
        public async Task GetGiftForUser_ReturnsUsersFromServiceAsync()
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
                ToReturn = new List<Gift>
                {
                    gift
                }
            };
            var controller = new GiftsController(testService, Mapper.Instance);

            var result = await controller.GetGiftForUser(4) as OkObjectResult;

            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            GiftViewModel resultGift = ((List<GiftViewModel>)result.Value).Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
            Assert.AreEqual(gift.Title, resultGift.Title);
            Assert.AreEqual(gift.Description, resultGift.Description);
            Assert.AreEqual(gift.Url, resultGift.Url);
            Assert.AreEqual(gift.OrderOfImportance, resultGift.OrderOfImportance);
        }

        [TestMethod]
        public async Task GetGiftForUser_RequiresPositiveUserIdAsync()
        {
            var testService = new TestableGiftService();
            var controller = new GiftsController(testService, Mapper.Instance);

            var result = await controller.GetGiftForUser(-1) as NotFoundResult;

            Assert.IsNotNull(result);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }
    }
}
