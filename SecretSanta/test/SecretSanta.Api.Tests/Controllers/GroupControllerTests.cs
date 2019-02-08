using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
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

        public GroupControllerTests()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGiftService()
        {
            IMapper mapper = Mapper.Instance;
            new GroupController(null, mapper);
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

            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            var result = controller.GetAllGroups() as OkObjectResult;

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
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);


            BadRequestResult result = controller.CreateGroup(null) as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateGroup_ConflictGroupAlreadyExist()
        {
            GroupInputViewModel viewModel = new GroupInputViewModel
            {
                Name = "The Titans"
            };
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.AddGroup(It.Is<Domain.Models.Group>(g => g.Name == viewModel.Name)))
                .Returns(new Domain.Models.Group
                {
                    Id = 2,
                    Name = viewModel.Name
                })
                .Verifiable();

            IMapper mapper = Mapper.Instance;
            GroupController controller = new GroupController(service.Object, mapper);

            controller.CreateGroup(viewModel);

            service.Setup(x => x.AddGroup(It.Is<Domain.Models.Group>(g => g.Name == viewModel.Name)))
                .Returns<IGroupService, Group>(null);

            ConflictObjectResult result = controller.CreateGroup(viewModel) as ConflictObjectResult;

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

            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            CreatedAtActionResult result = controller.CreateGroup(group) as CreatedAtActionResult;
            GroupViewModel value = result.Value as GroupViewModel;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, value.Id);
            Assert.AreEqual("Group", value.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);


            BadRequestResult result = controller.UpdateGroup(-1, null) as BadRequestResult;

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
            service.Setup(x => x.Find(2))
                .Returns(new Domain.Models.Group
                {
                    Id = 2,
                    Name = group.Name
                })
                .Verifiable();

            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            NoContentResult result = controller.UpdateGroup(2, group) as NoContentResult;

            Assert.IsNotNull(result);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void DeleteGroup_RequiresPositiveId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

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
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.DeleteGroup(2);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .Returns(true)
                .Verifiable();
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.DeleteGroup(2);

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.AddUserToGroup(groupId, 1);

            Assert.IsTrue(result is BadRequestResult);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public void AddUserToGroup_RequiresPositiveUserId(int userId)
        {
            var service = new Mock<IGroupService>(MockBehavior.Strict);
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.AddUserToGroup(1, userId);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void AddUserToGroup_WhenUserFailsToAddToGroupItReturnsNotFound()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(false)
                .Verifiable();
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.AddUserToGroup(2, 3);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void AddUserToGroup_ReturnsOkWhenUserAddedToGroup()
        {
            var service = new Mock<IGroupService>();
            service.Setup(x => x.AddUserToGroup(2, 3))
                .Returns(true)
                .Verifiable();
            IMapper mapper = Mapper.Instance;
            var controller = new GroupController(service.Object, mapper);

            IActionResult result = controller.AddUserToGroup(2, 3);

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
