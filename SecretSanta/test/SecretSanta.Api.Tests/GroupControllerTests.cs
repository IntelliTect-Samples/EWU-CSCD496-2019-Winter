using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGroupService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void GetUsersFromGroup_ReturnsUsersFromService()
        {
            User user = new User()
            {
                First = "Ringo",
                Last = "Ando",
                Id = 1
            };
            Group group = new Group()
            {
                Title = "Puyo Puyo Fans",
                Id = 1
            };
            UserGroup link = new UserGroup()
            {
                User = user,
                UserId = user.Id,
                Group = group,
                GroupId = group.Id
            };

            user.UserGroups.Add(link);
            group.UserGroups.Add(link);

            TestableGroupService service = new TestableGroupService()
            {
                ToReturnFindGroup = group,
                ToReturnGetUsersFromGroup = new List<User>() { user }
            };

            GroupController controller = new GroupController(service);

            ActionResult<List<DTO.User>> result = controller.GetUsersFromGroup(1);

            Assert.AreEqual(1, service.GroupId);
            DTO.User resultUser = result.Value.Single();
            Assert.AreEqual(user.First, resultUser.First);
            Assert.AreEqual(user.Last, resultUser.Last);
            Assert.AreEqual(user.Id, resultUser.Id);
        }

        [TestMethod]
        public void FetchAllGroups_ReturnGroupsFromGroup()
        {
            TestableGroupService service = new TestableGroupService()
            {
                ToReturnGetAllGroup = new List<Group>()
            };
            GroupController controller = new GroupController(service);

            Group group = new Group() { Title = "Battleship Battle Group 1" };

            service.ToReturnGetAllGroup.Add(group);

            ActionResult<List<DTO.Group>> result = controller.GetAllGroups();

            DTO.Group resultGroup = result.Value.Single();
            Assert.AreEqual(group.Title, resultGroup.Title);
        }

        [TestMethod]
        public void DeleteGroup_ReturnNotFound()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.DeleteGroup(-1);

            Assert.IsTrue(result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GroupId);
        }

        [TestMethod]
        public void DeleteGroup_ReturnOk()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.DeleteGroup(1);

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void MakeGroup_ReturnBadRequest()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.MakeGroup(null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GroupId);
        }

        [TestMethod]
        public void MakeGroup_ReturnOk()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.MakeGroup("Titans");

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void UpdateGroup_ReturnNotFound()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.UpdateGroup(-1, new DTO.Group());

            Assert.IsTrue(result is NotFoundResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GroupId);
        }

        [TestMethod]
        public void UpdateGroup_ReturnBadRequest()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.UpdateGroup(1, null);

            Assert.IsTrue(result is BadRequestResult);
            //This check ensures that the service was not called
            Assert.AreEqual(0, service.GroupId);
        }

        [TestMethod]
        public void UpdateGroup_ReturnOk()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.UpdateGroup(1, new DTO.Group());

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void AddUserToGroup_ReturnNotFound()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.AddUserToGroup(-1, 1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void AddUserToGroup_ReturnOk()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.AddUserToGroup(1, 1);

            Assert.IsTrue(result is OkResult);
        }

        [TestMethod]
        public void RemoveUserFromGroup_ReturnNotFound()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.RemoveUserFromGroup(-1, 1);

            Assert.IsTrue(result is NotFoundResult);
        }

        [TestMethod]
        public void RemoveUserFromGroup_ReturnOk()
        {
            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);

            ActionResult result = controller.RemoveUserFromGroup(1, 1);

            Assert.IsTrue(result is OkResult);
        }
    }
}
