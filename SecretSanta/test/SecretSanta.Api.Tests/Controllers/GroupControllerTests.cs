﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OkObjectResult = Microsoft.AspNetCore.Mvc.OkObjectResult;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGiftService()
        {
            new GroupController(null, Mapper.Instance);
        }

        [TestMethod]
        public void GetAllGroups_ReturnsGroups()
        {
            var group1 = new Group
            {
                Id = 1,
                Name = "Group 1"
            };
            var group2 = new Group
            {
                Id = 2,
                Name = "Group 2"
            };

            var service = new Mock<IGroupService>();
            service.Setup(x => x.FetchAll())
                .Returns(new List<Group> { group1, group2 })
                .Verifiable();


            var controller = new GroupController(service.Object, Mapper.Instance);

            OkObjectResult result = controller.GetAllGroups() as OkObjectResult;

            List<GroupViewModel> groups = result.Value as List<GroupViewModel>;
            Assert.AreEqual(2, groups.Count);
            AssertAreEqual(groups[0], group1);
            AssertAreEqual(groups[1], group2);
            service.VerifyAll();
        }

        [TestMethod]
        public void CreateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);


            ActionResult<GroupViewModel> result = controller.CreateGroup(null) as ActionResult;

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void CreateGroup_ReturnsCreatedGroup()
        {
            var group = new GroupInputViewModel
            {
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddGroup(It.Is<Domain.Models.Group>(g => g.Name == group.Name)))
                .Returns(new Domain.Models.Group
                {
                    Id = 2,
                    Name = group.Name
                })
                .Verifiable();

            var controller = new GroupController(service.Object, Mapper.Instance);

            OkObjectResult result = controller.CreateGroup(group) as OkObjectResult;

            GroupViewModel groupViewModel = result.Value as GroupViewModel;
            Assert.AreEqual(2, groupViewModel.Id);
            Assert.AreEqual("Group", groupViewModel.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);


            ActionResult<GroupViewModel> result = controller.UpdateGroup(-1, null) as ActionResult;

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateGroup_ReturnsUpdatedGroup()
        {
            var group = new GroupInputViewModel
            {
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.Find(2)).Returns(new Group { Id = 2, Name = "My Group" }).Verifiable();
            service.Setup(x => x.UpdateGroup(It.Is<Group>(g =>
                    g.Name == group.Name)))
                .Returns(new Domain.Models.Group
                {
                    Id = 2,
                    Name = group.Name
                })
                .Verifiable();

            var controller = new GroupController(service.Object, Mapper.Instance);

            OkObjectResult result = controller.UpdateGroup(2, group) as OkObjectResult;
            var groupViewModel = result.Value as GroupViewModel;

            Assert.AreEqual(2, groupViewModel.Id);
            Assert.AreEqual("Group", groupViewModel.Name);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.DeleteGroup(groupId) as ActionResult;

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public void DeleteGroup_ReturnsNotFoundWhenTheGroupFailsToDelete()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(false)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.DeleteGroup(2) as ActionResult;

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(true)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.DeleteGroup(2) as ActionResult;

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.AddUserToGroup(groupId, 1) as ActionResult;

            Assert.IsTrue(result is BadRequestResult);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.AddUserToGroup(1, userId) as ActionResult;

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void AddUserToGroup_WhenUserFailsToAddToGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(false)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.AddUserToGroup(2, 3) as ActionResult;

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void AddUserToGroup_ReturnsOkWhenUserAddedToGroup()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(true)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult result = controller.AddUserToGroup(2, 3) as ActionResult;

            Assert.IsTrue(result is OkResult);
        }

        private static void AssertAreEqual(GroupViewModel expected, Group actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
