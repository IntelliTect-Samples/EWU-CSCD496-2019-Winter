using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void GetUserForGroup_ReturnsUsersFromService()
        {
            var user = new User
            {
                Id = 3,
                FirstName = "Alan",
                LastName = "Watts"//,
                //Gifts = new List<Gift>(),
                //UserGroups = new List<UserGroups>(),
            };

            var testService = new TestableUserService
            {
                ToReturn = new List<User>
                {
                    user
                }
            };

            var controller = new UserController(testService);

            ActionResult<List<DTO.User>> result = controller.GetUserForGroup(3);

            Assert.AreEqual(3, testService.GetUsersForGroup_GroupId);
            DTO.User resultUser = result.Value.Single();
            Assert.AreEqual(user.Id, resultUser.Id);
            Assert.AreEqual(user.FirstName, resultUser.FirstName);
            Assert.AreEqual(user.LastName, resultUser.LastName);
        }

        [TestMethod]
        public void CreateUser()
        {
            var user = new User
            {
                Id = 0,
                FirstName = "Alan",
                LastName = "Watts"
            };

            var testService = new TestableUserService();
            testService.CreateUser(user);

            var controller = new UserController(testService);

            ActionResult result = controller.CreateUser(new DTO.User(user));
            Assert.AreEqual(user.Id, testService.CreateUser_User.Id);
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }

        [TestMethod]
        public void CreateUser_InvalidId()
        {
            var user = new User
            {
                Id = 3,
                FirstName = "Alan",
                LastName = "Watts"
            };

            var testService = new TestableUserService();
            testService.CreateUser(user);

            var controller = new UserController(testService);

            ActionResult result = controller.CreateUser(new DTO.User(user));
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }

        [TestMethod]
        public void UpdateUser()
        {
            var user = new User
            {
                Id = 3,
                FirstName = "Alan",
                LastName = "Watts"
            };

            var testService = new TestableUserService();
            testService.CreateUser(user);
            var controller = new UserController(testService);

            ActionResult result = controller.CreateUser(new DTO.User(user));

            Assert.AreEqual("Alan", testService.CreateUser_User.FirstName);

            user.FirstName = "Bill";

            testService.UpdateUser(user, 3);

            result = controller.UpdateUser(new DTO.User(user));

            Assert.AreEqual(user.FirstName, testService.UpdateUser_User.FirstName);
            Assert.AreEqual(3, testService.UpdateUser_UserId);
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }

        [TestMethod]
        public void DeleteUser()
        {
            var user = new User
            {
                Id = 3,
                FirstName = "Alan",
                LastName = "Watts"
            };

            var testService = new TestableUserService();
            testService.CreateUser(user);
            var controller = new UserController(testService);
            ActionResult result = controller.CreateUser(new DTO.User(user));

            testService.DeleteUser(user);
            result = controller.DeleteUser(new DTO.User(user));

            Assert.AreEqual(3, testService.DeleteUser_User.Id);
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }
    }
}
