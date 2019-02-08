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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGiftService()
        {
            GroupController groupController = new GroupController(null, null);
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

            var result = controller.Get() as OkObjectResult;

            List < GroupViewModel > groups = ((IEnumerable<GroupViewModel>)result.Value).ToList();

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


            var result = controller.UpdateGroup(1, null) as BadRequestObjectResult;

            Assert.IsNotNull(result);
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

            GroupController controller = new GroupController(service.Object, Mapper.Instance);

            CreatedAtActionResult result = controller.CreateGroup(group) as CreatedAtActionResult;
            GroupViewModel resultObj = result.Value as GroupViewModel;

            Assert.AreEqual(2, resultObj.Id);
            Assert.AreEqual("Group", resultObj.Name);
            service.VerifyAll();

        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);


            var result = controller.UpdateGroup(1, null);

            Assert.IsTrue(result is BadRequestObjectResult);

        }

        [TestMethod]
        public void UpdateGroup_ReturnsUpdatedGroup()
        {
            var group = new GroupInputViewModel
            {
                Name = "Group"
            };
            var service = new Mock<IGroupService>();
            service.Setup(x => x.Find(1)).Returns(new Group { Id = 1, Name = "My Group" }).Verifiable();
            service.Setup(x => x.UpdateGroup(It.Is<Group>(g =>
                    g.Name == group.Name)))
                .Returns(new Domain.Models.Group
                {
                    Id = 1,
                    Name = group.Name
                })
                .Verifiable();

            var controller = new GroupController(service.Object, Mapper.Instance);

            IActionResult result = controller.UpdateGroup(1, group);

            Assert.IsTrue(result is OkObjectResult);

            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            IActionResult result = controller.DeleteGroup(groupId);

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

            Assert.IsTrue(controller.DeleteGroup(2) is NotFoundObjectResult);
        }

        [TestMethod]
        public void DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(true)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            Assert.IsTrue(controller.DeleteGroup(2) is OkObjectResult);
        }

        [TestMethod]
        [DataRow(-1)]
        public void AddUserToGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            Assert.IsTrue(controller.AddUserToGroup(groupId, 1) is BadRequestObjectResult);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            Assert.IsTrue(controller.AddUserToGroup(1, userId) is BadRequestObjectResult);
        }

        [TestMethod]
        public void AddUserToGroup_WhenUserFailsToAddToGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(false)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            Assert.IsTrue(controller.AddUserToGroup(2, 3) is NotFoundObjectResult);
        }

        [TestMethod]
        public void AddUserToGroup_ReturnsOkWhenUserAddedToGroup()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(true)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            Assert.IsNotNull(controller.AddUserToGroup(2, 3) as OkObjectResult);
        }

        [TestMethod]
        public void CreateGroup_CompletesUnsuccessfully_EmptyName()
        {
            Group group = new Group
            {
                Name = "",
                Id = 25
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(group, new System.ComponentModel.DataAnnotations.ValidationContext(group), validationResults, true);

            Assert.IsFalse(actual, "Value must be null.");
            Assert.AreEqual(1, validationResults.Count, "Expected Error from Empty First Name.");
        }

        [TestMethod]
        public void CreateGroup_CompletesSuccessfully_NonEmptyName()
        {
            Group group = new Group
            {
                Name = "Dark Magician",
                Id = 25
            };

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool actual = Validator.TryValidateObject(group, new System.ComponentModel.DataAnnotations.ValidationContext(group), validationResults, true);

            Assert.IsTrue(actual, "Value must be an instance.");
            Assert.AreEqual(0, validationResults.Count, "Expected no Error from Non-Empty First Name.");
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
