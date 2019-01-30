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
    public class UserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void GetUser_ReturnsUserFromService()
        {
            User user = new User()
            {
                Id = 5,
                First = "Brad",
                Last = "Howard"
            };
            TestableUserService service = new TestableUserService()
            {
                ToReturn = new List<User>()
                {
                    user
                }
            };
            UserController controller = new UserController(service);

            ActionResult<DTO.User> result = controller.FindUser(5);

            Assert.AreEqual<int>(5, service.UserId);
            Assert.AreEqual<string>(user.First, result.Value.First);
            Assert.AreEqual<string>(user.Last, result.Value.Last);
        }

        [TestMethod]
        public void GetUser_ReturnNotFound()
        {
            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            ActionResult<DTO.User> result = controller.FindUser(-1);

            Assert.IsTrue(result.Result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.UserId);
        }

        [TestMethod]
        public void MakeUser_ReturnsOk()
        {
            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);
            
            ActionResult result = controller.MakeUser("Name: The Dude");

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void MakeUser_ReturnBadRequest()
        {
            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            ActionResult result = controller.MakeUser(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_ReturnOk()
        {
            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            DTO.User user = new DTO.User() { First = "The", Last = "Dude" };

            ActionResult result = controller.UpdateUser(1, user);

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void UpdateUser_ReturnNotFound()
        {

            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            DTO.User user = new DTO.User() { First = "The", Last = "Dude" };

            ActionResult result = controller.UpdateUser(-1, user);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void UpdateUser_ReturnBadRequest()
        {

            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            DTO.User user = new DTO.User() { First = "The", Last = "Dude" };

            ActionResult result = controller.UpdateUser(1, null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void DeleteUser_RetrunNotFound()
        {

            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            ActionResult result = controller.DeleteUser(-1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void DeleteUser_RetrunOk()
        {
            TestableUserService service = new TestableUserService();
            UserController controller = new UserController(service);

            ActionResult result = controller.DeleteUser(1);

            Assert.IsTrue(result is OkResult);
        }
    }
}
