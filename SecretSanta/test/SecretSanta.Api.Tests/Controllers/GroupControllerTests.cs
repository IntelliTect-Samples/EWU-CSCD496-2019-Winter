﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        public async Task GetAllGroups_ReturnsGroups()
        {
            Group group1 = new Group
            {
                Id = 1,
                Name = "Group 1"
            };
            Group group2 = new Group
            {
                Id = 2,
                Name = "Group 2"
            };

            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.FetchAll())
                .Returns(Task.FromResult(new List<Group> { group1, group2 }))
                .Verifiable();


            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            OkObjectResult result = (await controller.GetGroups().ConfigureAwait(false)).Result as OkObjectResult;

            List<GroupViewModel> groups = ((IEnumerable<GroupViewModel>)result.Value).ToList();

            Assert.AreEqual(2, groups.Count);
            AssertAreEqual(groups[0], group1);
            AssertAreEqual(groups[1], group2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateGroup_RequiresGroup()
        {
            Mock<IGroupService> service = new Mock<IGroupService>(MockBehavior.Strict);
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            BadRequestResult result = (await controller.CreateGroup(null).ConfigureAwait(false)).Result as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateGroup_ReturnsCreatedGroup()
        {
            GroupInputViewModel group = new GroupInputViewModel
            {
                Name = "Group"
            };
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.AddGroup(It.Is<Group>(g => g.Name == group.Name)))
                .Returns(Task.FromResult(new Group
                {
                    Id = 2,
                    Name = group.Name
                }))
                .Verifiable();

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            CreatedAtActionResult result = (await controller.CreateGroup(group).ConfigureAwait(false)).Result as CreatedAtActionResult;
            GroupViewModel resultValue = result.Value as GroupViewModel;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.Id);
            Assert.AreEqual("Group", resultValue.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task UpdateGroup_RequiresGroup()
        {
            Mock<IGroupService> service = new Mock<IGroupService>(MockBehavior.Strict);
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);


            BadRequestResult result = (await controller.UpdateGroup(1, null).ConfigureAwait(false)) as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateGroup_ReturnsUpdatedGroup()
        {
            GroupInputViewModel group = new GroupInputViewModel
            {
                Name = "Group"
            };
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.GetById(2))
                .Returns(Task.FromResult(new Group
                {
                    Id = 2,
                    Name = group.Name
                }))
                .Verifiable();

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            NoContentResult result = (await controller.UpdateGroup(2, group).ConfigureAwait(false)) as NoContentResult;

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteGroup_RequiresPositiveId(int groupId)
        {
            Mock<IGroupService> service = new Mock<IGroupService>(MockBehavior.Strict);
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            ActionResult result = await controller.DeleteGroup(groupId).ConfigureAwait(false);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsNotFoundWhenTheGroupFailsToDelete()
        {
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            ActionResult result = await controller.DeleteGroup(2).ConfigureAwait(false);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            ActionResult result = await controller.DeleteGroup(2).ConfigureAwait(false);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
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
