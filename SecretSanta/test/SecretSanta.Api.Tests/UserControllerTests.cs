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
        private TestableUserService TestService { get; set; }
        private UserController UserController { get; set; }

        [TestInitialize]
        public void InitializeController()
        {
            TestService = new TestableUserService { UsersToReturn = new List<User> { } };
            UserController = new UserController(TestService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void AddUser_ReturnsUser()
        {
            var user = CreateUser(1, "Billy", "Bobby");
            ActionResult<DTO.User> result = UserController.AddUser(new DTO.User(user));

            Assert.AreEqual(1, TestService.LastModifiedUser.Id);

            DTO.User resultUser = result.Value;
            Assert.AreEqual<int>(user.Id, TestService.LastModifiedUser.Id);
            Assert.AreEqual<int>(user.Id, resultUser.Id);
            Assert.AreEqual<string>(user.FirstName, resultUser.FirstName);
        }

        [TestMethod]
        public void AddUser_UserCannotBeNull()
        {
            ActionResult<DTO.User> result = UserController.AddUser(null);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void UpdateUser_UserIsUpdated()
        {
            var user = CreateUser(2, "Billy", "Bobby");
            ActionResult<DTO.User> result = UserController.UpdateUser(new DTO.User(user));

            DTO.User resultUser = result.Value;

            Assert.AreEqual<int>(user.Id, TestService.LastModifiedUser.Id);
            Assert.AreEqual<int>(user.Id, resultUser.Id);
            Assert.AreEqual<string>(user.FirstName, resultUser.FirstName);
            Assert.AreEqual<string>(user.LastName, resultUser.LastName);
        }

        [TestMethod]
        public void UpdateUser_UserCannotBeNull()
        {
            ActionResult<DTO.User> result = UserController.UpdateUser(null);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteUser_UserIsRemoved()
        {
            var user = CreateUser(2, "Billy", "Bobby");
            ActionResult<DTO.User> result = UserController.DeleteUser(new DTO.User(user));

            DTO.User resultUser = result.Value;

            Assert.AreEqual<int>(user.Id, TestService.LastModifiedUser.Id);
            Assert.AreEqual<int>(user.Id, resultUser.Id);
            Assert.AreEqual<string>(user.FirstName, resultUser.FirstName);
            Assert.AreEqual<string>(user.LastName, resultUser.LastName);
        }

        [TestMethod]
        public void DeleteUser_UserCannotBeNull()
        {
            ActionResult<DTO.User> result = UserController.DeleteUser(null);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        private User CreateUser(int id, string fName, string lName)
        {
            return new User
            {
                FirstName = fName,
                LastName = lName,
                Id = id,
                Gifts = new List<Gift>(),
                GroupUsers = new List<GroupUser>()
            };
        }
    }
}