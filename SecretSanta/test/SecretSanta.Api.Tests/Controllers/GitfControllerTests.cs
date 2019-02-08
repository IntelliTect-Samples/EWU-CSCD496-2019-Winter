using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

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
            var controller = new GiftController(testService, Mapper.Instance);

            var result = controller.GetGiftForUser(4) as OkObjectResult;

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
            var testService = new TestableGiftService();
            var controller = new GiftController(testService, Mapper.Instance);

            IActionResult result = controller.GetGiftForUser(-1);

            Assert.IsNotNull(result);
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetGiftsForUser_UserId);
        }

        [TestMethod]
        public void CreateGift_CompletesUnsuccessfully_EmptyName()
        {
            Gift gift = new Gift
            {
                Title = "",
                Id = 30
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(gift, new System.ComponentModel.DataAnnotations.ValidationContext(gift), validationResults, true);

            Assert.IsFalse(actual, "Value must be null.");
            Assert.AreEqual(1, validationResults.Count, "Expected Error from Empty First Name.");
        }

        [TestMethod]
        public void CreateGift_CompletesSuccessfully_NonEmptyName()
        {
            Gift gift = new Gift
            {
                Title = "Blue Eyes White Dragon",
                Id = 30
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(gift, new System.ComponentModel.DataAnnotations.ValidationContext(gift), validationResults, true);

            Assert.IsTrue(actual, "Value must be an instance.");
            Assert.AreEqual(0, validationResults.Count, "Expected no Error from Non-Empty First Name.");
        }
    }
}
