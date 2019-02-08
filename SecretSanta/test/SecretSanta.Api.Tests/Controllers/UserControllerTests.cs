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
        public void GetUserById_InvalidId()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService, Mapper.Instance);

            IActionResult result = controller.Get(-1);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void GetUserById()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService, Mapper.Instance);

            IActionResult result = controller.Get(1);

            Assert.IsTrue(result is OkObjectResult);
        }

        [TestMethod]
        public void AddUser_NoFirstName()
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
                LastName = "James"
            };

            var result = controller.Post(viewModel);

            Assert.IsTrue(result is BadRequestResult);
            Assert.AreEqual(viewModel.FirstName, null);
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

        [TestMethod]
        public void UpdateUser_RequiresPositiveUserId()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService, Mapper.Instance);

            var result = controller.Put(-1, null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(null, testService.UpdateUser_User);
        }

        [TestMethod]
        public void UpdateUser_Successfully()
        {
            var user = new Domain.Models.User
            {
                Id = 4,
                FirstName = "Lebron",
                LastName = "James"
            };

            var testService = new TestableUserService();

            testService.UpdateUser_User = user;

            user.FirstName = "Lebron Jr";

            testService.UpdateUser(user);

            var controller = new UserController(testService, Mapper.Instance);

            var viewModel = new UserInputViewModel
            {
                FirstName = "Lebron Jr",
                LastName = "James"
            };

            var result = controller.Put(1, viewModel);

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(user.FirstName, testService.UpdateUser_User.FirstName);
        }

        [TestMethod]
        public void RemoveUser_ReturnsTrue()
        {
            var testService = new TestableUserService();

            var controller = new UserController(testService, Mapper.Instance);

            var viewModel = new UserInputViewModel
            {
                FirstName = "Lebron",
                LastName = "James"
            };

            testService.DeleteUser_Bool = true;
            IActionResult result = controller.Delete(1);

            Assert.IsTrue(result is OkResult);
            Assert.IsTrue(testService.DeleteUser_Bool);
        }

        [TestMethod]
        public void RemoveUser_InvalidUserId()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService, Mapper.Instance);

            testService.DeleteUser_Bool = false;
            IActionResult result = controller.Delete(-1);

            Assert.IsTrue(result is NotFoundResult);
            Assert.IsFalse(testService.DeleteUser_Bool);
        }
    }
}
