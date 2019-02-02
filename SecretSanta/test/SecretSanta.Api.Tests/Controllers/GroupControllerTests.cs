using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGiftService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void GetAllGroups_ReturnsGroups()
        {
            var group1 = new Domain.Models.Group
            {
                Id = 1,
                Name = "Group 1"
            };
            var group2 = new Domain.Models.Group
            {
                Id = 2,
                Name = "Group 2"
            };

            var service = new Mock<IGroupService>();
            service.Setup(x => x.FetchAll())
                .Returns(new List<Domain.Models.Group>{ group1, group2})
                .Verifiable();


            var controller = new GroupController(service.Object);

            ActionResult<IEnumerable<Group>> result = controller.GetAllGroups();

            List<Group> groups = result.Value.ToList();
            
            Assert.AreEqual(2, groups.Count);
            AssertAreEqual(groups[0], group1);
            AssertAreEqual(groups[1], group2);
            service.VerifyAll();
        }

        [TestMethod]
        public void CreateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object);


            ActionResult<Group> result = controller.CreateGroup(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void CreateGroup_ReturnsCreatedGroup()
        {
            var group = new Group
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

            var controller = new GroupController(service.Object);

            ActionResult<Group> result = controller.CreateGroup(group);

            Assert.AreEqual(2, result.Value.Id);
            Assert.AreEqual("Group", result.Value.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object);


            ActionResult<Group> result = controller.UpdateGroup(null);

            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateGroup_ReturnsUpdatedGroup()
        {
            var group = new Group
            {
                Id = 2,
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.UpdateGroup(It.Is<Domain.Models.Group>(g => 
                    g.Id == group.Id &&
                    g.Name == group.Name)))
                .Returns(new Domain.Models.Group
                {
                    Id = group.Id,
                    Name = group.Name
                })
                .Verifiable();

            var controller = new GroupController(service.Object);

            ActionResult<Group> result = controller.UpdateGroup(group);

            Assert.AreEqual(2, result.Value.Id);
            Assert.AreEqual("Group", result.Value.Name);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object);

            ActionResult result = controller.DeleteGroup(groupId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public void DeleteGroup_ReturnsNotFoundWhenTheGroupFailsToDelete()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(false)
                .Verifiable();
            var controller = new GroupController(service.Object);

            ActionResult result = controller.DeleteGroup(2);

            Assert.IsTrue(result is NotFoundResult);
            service.VerifyAll();
        }

        [TestMethod]
        public void DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(true)
                .Verifiable();
            var controller = new GroupController(service.Object);

            ActionResult result = controller.DeleteGroup(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        private static void AssertAreEqual(Group expected, Domain.Models.Group actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null) Assert.Fail();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
