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

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        /*[AssemblyInitialize]
        public static void ConfigureAutoMapper(TestContext context)
        {
            Mapper.Initialize(config => config.AddProfile(new AutoMapperProfileConfigs()));
        }*/

        public GiftControllerTests()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
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
                ToReturn = new List<Gift>
                {
                    gift
                }
            };

            IMapper mapper = Mapper.Instance;

            GiftController controller = new GiftController(testService, mapper);

            OkObjectResult result = controller.GetGiftForUser(4) as OkObjectResult;

            Assert.AreEqual(4, testService.GetGiftsForUser_UserId);
            GiftViewModel resultGift = ((List<GiftViewModel>)result.Value).Single();
            Assert.AreEqual(gift.Id, resultGift.Id);
            Assert.AreEqual(gift.Title, resultGift.Title);
            Assert.AreEqual(gift.Description, resultGift.Description);
            Assert.AreEqual(gift.Url, resultGift.Url);
            Assert.AreEqual(gift.OrderOfImportance, resultGift.OrderOfImportance);
        }

        [TestMethod]
        public void GetGiftForUser_RequiresPositiveUserId()
        {
            TestableGiftService testService = new TestableGiftService();
            IMapper mapper = Mapper.Instance;
            GiftController controller = new GiftController(testService, mapper);

            NotFoundResult result = controller.GetGiftForUser(-1) as NotFoundResult;

            Assert.IsNotNull(result);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }
    }
}
