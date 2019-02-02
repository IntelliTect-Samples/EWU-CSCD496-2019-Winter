using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupUserControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupUserController_RequiresGiftService()
        {
            new GroupUserController(null);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.AddUserToGroup(groupId, 1);

            Assert.IsTrue(result is BadRequestResult);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.AddUserToGroup(1, userId);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void AddUserToGroup_WhenUserFailsToAddToGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(false)
                .Verifiable();
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.AddUserToGroup(2, 3);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public void AddUserToGroup_ReturnsOkWhenUserAddedToGroup()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(true)
                .Verifiable();
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.AddUserToGroup(2, 3);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void RemoveUserFromGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.RemoveUserFromGroup(groupId, 1);

            Assert.IsTrue(result is BadRequestResult);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void RemoveUserFromGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.RemoveUserFromGroup(1, userId);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void RemoveUserFromGroup_WhenUserFailsToRemoveFromGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.RemoveUserFromGroup(2, 3))
                .Returns(false)
                .Verifiable();
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.RemoveUserFromGroup(2, 3);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public void RemoveUserFromGroup_ReturnsOkWhenUserRemovedFromGroup()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.RemoveUserFromGroup(2, 3))
                .Returns(true)
                .Verifiable();
            var controller = new GroupUserController(service.Object);

            ActionResult result = controller.RemoveUserFromGroup(2, 3);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }
    }
}
