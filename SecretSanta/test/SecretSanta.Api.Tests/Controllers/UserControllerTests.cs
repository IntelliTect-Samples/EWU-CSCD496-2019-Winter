using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private AutoMocker _Mocker;
        private Mock<IUserService> UserService => _Mocker.GetMock<IUserService>();

        [TestInitialize]
        public void TestInit()
        {
            _Mocker = new AutoMocker(MockBehavior.Strict);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserController_RequiresGiftService()
        {
            new UserController(null);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsUsers()
        {
            var user1 = new Domain.Models.User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            var user2 = new Domain.Models.User
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith"
            };
            
            UserService.Setup(x => x.FetchAll())
                .Returns(new List<Domain.Models.User> { user1, user2 })
                .Verifiable();

            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult<IEnumerable<User>> result = controller.GetAllUsers();

            List<User> users = result.Value.ToList();

            Assert.AreEqual(2, users.Count);
            AssertAreEqual(users[0], user1);
            AssertAreEqual(users[1], user2);
            _Mocker.VerifyAll();
        }

        [TestMethod]
        public void CreateUser_RequiresUser()
        {
            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult<User> result = controller.CreateUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void CreateUser_ReturnsCreatedUser()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };
            UserService.Setup(x => x.AddUser(It.Is<Domain.Models.User>(u => u.FirstName == user.FirstName && u.LastName == user.LastName)))
                .Returns(new Domain.Models.User
                {
                    Id = 2,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                })
                .Verifiable();

            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult<User> result = controller.CreateUser(user);

            Assert.AreEqual(2, result.Value.Id);
            Assert.AreEqual("John", result.Value.FirstName);
            Assert.AreEqual("Doe", result.Value.LastName);
            _Mocker.VerifyAll();
        }

        [TestMethod]
        public void UpdateUser_RequiresUser()
        {
            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult<User> result = controller.UpdateUser(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateUser_ReturnsUpdatedUser()
        {
            var user = new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe"
            };
            UserService.Setup(x => x.UpdateUser(It.Is<Domain.Models.User>(u =>
                    u.Id == user.Id &&
                    u.FirstName == user.FirstName &&
                    u.LastName == user.LastName)))
                .Returns(new Domain.Models.User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                })
                .Verifiable();

            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult<User> result = controller.UpdateUser(user);

            Assert.AreEqual(2, result.Value.Id);
            Assert.AreEqual("John", result.Value.FirstName);
            Assert.AreEqual("Doe", result.Value.LastName);
            _Mocker.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteUser_RequiresPositiveId(int userId)
        {
            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult result = controller.DeleteUser(userId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public void DeleteUser_ReturnsNotFoundWhenTheUserFailsToDelete()
        {
            UserService.Setup(x => x.DeleteUser(2))
                .Returns(false)
                .Verifiable();
            var controller = _Mocker.CreateInstance<UserController>();
            
            ActionResult result = controller.DeleteUser(2);

            Assert.IsTrue(result is NotFoundResult);
            _Mocker.VerifyAll();
        }

        [TestMethod]
        public void DeleteUser_ReturnsOkWhenUserIsDeleted()
        {
            UserService.Setup(x => x.DeleteUser(2))
                .Returns(true)
                .Verifiable();
            var controller = _Mocker.CreateInstance<UserController>();

            ActionResult result = controller.DeleteUser(2);

            Assert.IsTrue(result is OkResult);
            _Mocker.VerifyAll();
        }

        private static void AssertAreEqual(User expected, Domain.Models.User actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }
    }
}
