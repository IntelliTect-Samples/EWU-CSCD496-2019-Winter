using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            var userController = new UserController(null);
        }

        [TestMethod]
        public void AddUser_RequiresUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            var result = controller.AddUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);

            // Make sure UserService was not called
            Assert.IsNull(testService.AddUser_User);
        }

        [TestMethod]
        public void AddUser_InvokesService()
        {
            var userDto = new User
            {
                Id = 42,
                FirstName = "Cameron",
                LastName = "Osborn"
            };
            var testService = new TestableUserService {AddUser_Return = User.ToEntity(userDto)};
            var controller = new UserController(testService);

            var result = controller.AddUser(userDto);

            Assert.IsNotNull(result, "Result was not a 200");

            // Ensure service was called
            Assert.AreEqual(userDto.Id, testService.AddUser_User.Id);
        }

        [TestMethod]
        public void UpdateUser_RequiresUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            var result = controller.UpdateUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);

            // Make sure UserService was not called
            Assert.IsNull(testService.AddUser_User);
        }

        [TestMethod]
        public void UpdateUser_InvokesService()
        {
            var userDto = new User
            {
                Id = 42,
                FirstName = "Cameron",
                LastName = "Osborn"
            };
            var testService = new TestableUserService {UpdateUser_Return = User.ToEntity(userDto)};
            var controller = new UserController(testService);

            var result = controller.UpdateUser(userDto);

            Assert.IsNotNull(result, "Result was not a 200");

            // Ensure service was called
            Assert.AreEqual(userDto.Id, testService.UpdateUser_User.Id);
        }
    }
}