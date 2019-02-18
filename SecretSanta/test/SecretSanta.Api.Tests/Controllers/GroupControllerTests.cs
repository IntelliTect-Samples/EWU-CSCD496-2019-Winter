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

            //IActionResult result = await Task.Run(() => controller.Get());

            IActionResult result = await controller.Get();

            OkObjectResult resultAsOk = result as OkObjectResult;

            List<GroupViewModel> groups = ((IEnumerable<GroupViewModel>)resultAsOk.Value).ToList();

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

            IActionResult task = await controller.Post(null);
            BadRequestResult result = task as BadRequestResult;

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
                .ReturnsAsync(new Group
                {
                    Id = 2,
                    Name = group.Name
                })
                .Verifiable();

            /*.Returns(Task.Run(() => new Group
            {
                Id = 2,
                Name = group.Name
            }))
            .Verifiable();
            */

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Post(group);

            CreatedAtActionResult resultAsCreationResult = result as CreatedAtActionResult;
            GroupViewModel resultAsGroupView = resultAsCreationResult.Value as GroupViewModel;

            Assert.IsNotNull(resultAsGroupView);
            Assert.AreEqual(2, resultAsGroupView.Id);
            Assert.AreEqual("Group", resultAsGroupView.Name);
            service.VerifyAll();
        }

        [TestMethod]
        public async Task UpdateGroup_RequiresGroup()
        {
            Mock<IGroupService> service = new Mock<IGroupService>(MockBehavior.Strict);

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Put(1, null);

            BadRequestResult resultAsBadRequest = result as BadRequestResult;

            Assert.IsNotNull(resultAsBadRequest);
        }

        [TestMethod]
        public async Task UpdateGroup_ReturnsUpdatedGroup()
        {
            GroupInputViewModel group = new GroupInputViewModel
            {
                Name = "Group"
            };

            Mock<IGroupService> service = new Mock<IGroupService>();

            service.Setup( x => x.GetById(2) )
                                            .ReturnsAsync(new Group
                                                                    {
                                                                        Id = 2,
                                                                        Name = group.Name
                                                                    }
                                                              )
                                                     .Verifiable();

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Put(2, group);

            NoContentResult resultAsNoContentResult = result as NoContentResult;

            Assert.IsNotNull(resultAsNoContentResult);
            service.VerifyAll();
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task DeleteGroup_RequiresPositiveId(int groupId)
        {
            Mock<IGroupService> service = new Mock<IGroupService>(MockBehavior.Strict);
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Delete(groupId);

            bool isResultBadRequestObj = result is BadRequestObjectResult;

            Assert.IsTrue(isResultBadRequestObj);
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsNotFoundWhenTheGroupFailsToDelete()
        {
            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.DeleteGroup(2))
                .ReturnsAsync(false)
                .Verifiable();
            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Delete(2);

            bool isResultNotFound = result is NotFoundResult;

            Assert.IsTrue(isResultNotFound);

            service.VerifyAll();
        }

        [TestMethod]
        public async Task DeleteGroup_ReturnsOkWhenGroupIsDeleted()
        {
            Mock<IGroupService> service = new Mock<IGroupService>();

            service.Setup(x => x.DeleteGroup(2))
                .ReturnsAsync(true)
                .Verifiable();

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            IActionResult result = await controller.Delete(2);

            bool isResultOk = result is OkResult;

            Assert.IsTrue(isResultOk);

            service.VerifyAll();
        }

        private static void AssertAreEqual(GroupViewModel expected, Group actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            else if (expected == null || actual == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
            }
        }
    }
}
