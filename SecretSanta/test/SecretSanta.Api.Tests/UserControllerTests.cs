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
                LastName = "Watts",
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
                Id = 3,
                FirstName = "Alan",
                LastName = "Watts"
            };

            var testService = new TestableUserService
            {
                User = user
            };

            var controller = new UserController(testService);

            DTO.User resultUser = controller.CreateUser(new DTO.User(user)).Value;
            Assert.AreEqual(user.Id, resultUser.Id);
            Assert.AreEqual(user.FirstName, resultUser.FirstName);
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

            var testService = new TestableUserService { User = user };
            var controller = new UserController(testService);

            DTO.User resultUser = controller.CreateUser(new DTO.User(user)).Value;

            Assert.AreEqual("Alan", resultUser.FirstName);

            resultUser.FirstName = "Hello";
            resultUser.LastName = "World";

            DTO.User updatedUser = controller.UpdateUser(resultUser, resultUser.Id).Value;

            Assert.AreEqual(resultUser.FirstName, updatedUser.FirstName);
            Assert.AreNotEqual(user.FirstName, updatedUser.FirstName);
        }
    }
}
