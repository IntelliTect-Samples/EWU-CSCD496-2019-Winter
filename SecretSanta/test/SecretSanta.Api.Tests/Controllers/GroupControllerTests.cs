using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
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

            var result = (OkObjectResult) controller.GetAllGroups();

            List<GroupViewModel> groups = ((IEnumerable<GroupViewModel>)result.Value).ToList();

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


            var result = (BadRequestResult)controller.CreateGroup(null);

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

            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = (CreatedAtActionResult) controller.CreateGroup(group);
            var resultValue = (GroupViewModel)result.Value;

            Assert.IsNotNull(resultValue);
            Assert.AreEqual(2, resultValue.Id);
            Assert.AreEqual("Group", resultValue.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            ActionResult<GroupViewModel> result = (BadRequestResult) controller.UpdateGroup(-1, null);

            Assert.IsNotNull(result);
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

            ActionResult<GroupViewModel> result = (CreatedAtActionResult)controller.UpdateGroup(2, group);

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.DeleteGroup(groupId);

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

            var result = controller.DeleteGroup(2);

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
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.DeleteGroup(2);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.AddUserToGroup(groupId, 1);

            Assert.IsTrue(result is BadRequestResult);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.AddUserToGroup(1, userId);

            Assert.IsTrue(result is BadRequestResult);
            service.VerifyAll();
        }

        [TestMethod]
        public void AddUserToGroup_WhenUserFailsToAddToGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(false)
                .Verifiable();
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.AddUserToGroup(2, 3);

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
            var controller = new GroupController(service.Object, Mapper.Instance);

            var result = controller.AddUserToGroup(2, 3);

            Assert.IsTrue(result is OkResult);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task CreateGroupViaApi_FailsDueToMissingName()
        {
            var client = Factory.CreateClient();

            var viewModel = new GroupInputViewModel
            {
                Name = ""
            };

            var response = await client.PostAsJsonAsync("/api/group", viewModel);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(result);

            var errors = problemDetails.Extensions["errors"] as JObject;

            var firstError = (JProperty)errors.First;

            var errorMessage = firstError.Value[0];

            Assert.AreEqual("The Name field is required.", ((JValue)errorMessage).Value);
        }

        [TestMethod]
        public async Task CreateGroupViaApi_FailsDueToNonUniqueName()
        {
            var client = Factory.CreateClient();

            var viewModel = new GroupInputViewModel
            {
                Name = "Group1"
            };

            var secondViewModel = new GroupInputViewModel
            {
                Name = "Group1"
            };

            var response = await client.PostAsJsonAsync("/api/group", viewModel);
            response = await client.PostAsJsonAsync("/api/group", secondViewModel);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
