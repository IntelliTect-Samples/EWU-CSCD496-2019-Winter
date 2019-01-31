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
        private User CreateUser()
        {
            return new User
            {
                Id = 8,
                FirstName = "Jack",
                LastName = "London",
                Gifts = new List<Gift>(),
                GroupUsers = new List<GroupUser>()
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresUserService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void AddUser_ReturnsUserAdded()
        {
            User newUser = CreateUser();

            TestableUserService service = new TestableUserService();
            service.AddUser(newUser);

            UserController userController = new UserController(service);
            ActionResult resultUser = userController.AddUser(new DTO.User (newUser) );

            Assert.AreEqual(newUser.Id, service.AddUser_User.Id);
            Assert.IsTrue(resultUser is OkObjectResult);
        }

        [TestMethod]
        public void AddUser_RequireUserInstance()
        {
            DTO.User nullUser = null;

            TestableUserService service = new TestableUserService();
            UserController userController = new UserController(service);
            ActionResult<List<DTO.User>> result = userController.AddUser(nullUser);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void DeleteUser_ReturnUserDelete()
        {
            User newUser = CreateUser();

            TestableUserService service = new TestableUserService();
            service.AddUser(newUser);

            UserController userController = new UserController(service);
            userController.AddUser(new DTO.User(newUser));

            service.RemoveUser(newUser);
            ActionResult result = userController.RemoveUser(new DTO.User(newUser));

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(newUser.Id, service.RemoveUser_User.Id);
        }

        [TestMethod]
        public void DeleteUser_RequireInstance()
        {
            User newUser = CreateUser();

            TestableUserService service = new TestableUserService();
            service.AddUser(newUser);

            UserController userController = new UserController(service);
            userController.AddUser(new DTO.User(newUser));

            service.RemoveUser(null);
            ActionResult <List<DTO.User>> result = userController.RemoveUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_ReturnUpdateUser()
        {
            User newUser = CreateUser();

            TestableUserService service = new TestableUserService();
            service.AddUser(newUser);

            UserController userController = new UserController(service);
            userController.AddUser(new DTO.User(newUser));

            Assert.AreEqual(service.AddUser_User.FirstName, "Jack");

            newUser.FirstName = "France";
            service.UpdateUser(newUser);

            ActionResult<List<DTO.User>> result = userController.UpdateUser(new DTO.User(newUser));

            Assert.AreEqual("France", service.UpdateUser_User.FirstName);
            Assert.IsTrue(result.Result is OkObjectResult);
        }

        [TestMethod]
        public void UpdateUser_RequireInstance()
        {
            User newUser = CreateUser();

            TestableUserService service = new TestableUserService();
            service.AddUser(newUser);

            UserController userController = new UserController(service);
            userController.AddUser(new DTO.User(newUser));

            Assert.AreEqual(service.AddUser_User.FirstName, "Jack");

            service.UpdateUser(null);

            ActionResult<List<DTO.User>> result = userController.UpdateUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }
        //Delete this comment
    }
}