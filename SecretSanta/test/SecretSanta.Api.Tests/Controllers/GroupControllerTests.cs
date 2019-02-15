using AutoMapper;
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
                .Returns(Task.FromResult(new List<Group> { group1, group2 }))
                .Verifiable();


            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Get() as OkObjectResult;

            List<GroupViewModel> groups = ((IEnumerable<GroupViewModel>)result.Value).ToList();

            Assert.AreEqual(2, groups.Count);
            AssertAreEqual(groups[0], group1);
            AssertAreEqual(groups[1], group2);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupsController(service.Object, Mapper.Instance);


            var result = await controller.Post(null) as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateGroup_ReturnsCreatedGroup()
        {
            var group = new GroupInputViewModel
            {
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddGroup(It.Is<Group>(g => g.Name == group.Name)))
                .Returns(Task.FromResult(new Group
                {
                    Id = 2,
                    Name = group.Name
                }))
                .Verifiable();

            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Post(group) as CreatedAtActionResult;
            var resultValue = result.Value as GroupViewModel;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.Id);
            Assert.AreEqual("Group", resultValue.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupsController(service.Object, Mapper.Instance);


            var result = await controller.Put(1, null) as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateGroup_ReturnsUpdatedGroup()
        {
            var group = new GroupInputViewModel
            {
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.GetById(2))
                .Returns(Task.FromResult(new Group
                {
                    Id = 2,
                    Name = group.Name
                }))
                .Verifiable();

            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Put(2, group) as NoContentResult;

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Delete(groupId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsNotFoundWhenTheGroupFailsToDelete()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(Task.FromResult(false))
                .Verifiable();
            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Delete(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(Task.FromResult(true))
                .Verifiable();
            var controller = new GroupsController(service.Object, Mapper.Instance);

            var result = await controller.Delete(2);

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
