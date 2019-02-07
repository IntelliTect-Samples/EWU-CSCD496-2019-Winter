using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [AssemblyInitialize]
        public static void ConfigureAutoMapper(TestContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
        }

        [TestMethod]
        public void AddUser_AddedSuccessfully()
        {
            var testService = new TestableUserService();

            testService.AddUser_User = new Domain.Models.User
            {
                FirstName = "Lebron",
                LastName = "James"
            };

            var controller = new UserController(testService, Mapper.Instance);

            var viewModel = new UserInputViewModel
            {
                FirstName = "Lebron",
                LastName = "James"
            };

            var result = controller.Post(viewModel);

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(viewModel.FirstName, testService.AddUser_User.FirstName);
            Assert.AreEqual(viewModel.LastName, testService.AddUser_User.LastName);
        }

        [TestMethod]
        public void AddUser_NullViewModel_ReturnsBadRequest()
        {
            var testService = new TestableUserService();

            testService.AddUser_User = null;

            var controller = new UserController(testService, Mapper.Instance);

            var result = controller.Post(null);

            Assert.IsTrue(result is BadRequestResult);
        }
    }
}
