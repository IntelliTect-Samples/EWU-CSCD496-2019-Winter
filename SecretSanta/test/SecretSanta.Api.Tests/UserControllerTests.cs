using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresGiftService()
        {
            new UserController(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FetchAll_NoUsers()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult<List<DTO.User>> result = controller.FetchAll();
        }

        [TestMethod]
        public void AddUser_NullUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.AddUser(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_NullUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.UpdateUser(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void RemoveUser_NullUser()
        {
            var testService = new TestableUserService();
            var controller = new UserController(testService);

            ActionResult result = controller.RemoveUser(null);

            Assert.IsTrue(result is BadRequestResult);
        }
    }
}
