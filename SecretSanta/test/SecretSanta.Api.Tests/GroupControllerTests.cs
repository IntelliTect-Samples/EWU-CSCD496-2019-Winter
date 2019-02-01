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
        private Group CreateGroup()
        {
            return new Group
            {
                Name = "Tyler",
                Id = 23
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGroupService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void MakeGroup_Success()
        {
            Group group = CreateGroup();

            TestableGroupService service = new TestableGroupService();
            Group addedGroup = service.AddGroup(group.Id, group);

            GroupController controller = new GroupController(service);

            ActionResult result = controller.CreateGroup(group.Id, new DTO.Group(group));

            Assert.IsTrue(result is OkObjectResult);
            Assert.AreEqual(group.Id, service.AddGroup_Group.Id);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void MakeGroup_InstanceRequired()
        {
            Group group =  null;

            TestableGroupService service = new TestableGroupService();
            GroupController controller = new GroupController(service);
            ActionResult result = controller.CreateGroup(group.Id, new DTO.Group(group));

            Assert.IsTrue(result is BadRequestResult);
        }

        /*[TestMethod]
        public void GetListOfAllGroups_Success()
        {
            Group group1 = CreateGroup();
            Group group2 = CreateGroup();
            group2.Name = "Johnny";

            TestableGroupService testService = new TestableGroupService
            {
                GetListOfGroup_Return = new List<Group>
                {
                    group1,
                    group2
                }
            };

            GroupController controller = new GroupController(testService);

            ActionResult<List<DTO.Group>> result = controller.GetListOfGroups();

            List<DTO.Group> resultGroups = result.Value;
            Assert.AreEqual(resultGroups[0].Name, group1.Name);
            Assert.AreEqual(resultGroups[1].Name, group2.Name);
        }*/

        /*[TestMethod]
        public void GetAllUsersInGroup_Success()
        {

        }*/

        [TestMethod]
        public void RemoveGroup_Success()
        {
            Group group = CreateGroup();

            TestableGroupService service = new TestableGroupService();
            Group addedGroup = service.AddGroup(group.Id, group);

            GroupController controller = new GroupController(service);

            ActionResult<DTO.Group> result = controller.CreateGroup(group.Id, new DTO.Group(group));

            Assert.AreEqual(group.Id, service.AddGroup_Group.Id);

            controller.DeleteGroup(group.Id, new DTO.Group(group));

            Assert.AreEqual(service.RemoveGroup_GroupId, group.Id);
        }


        [TestMethod]
        public void UpdateGroup_Success()
        {
            Group group = CreateGroup();

            TestableGroupService service = new TestableGroupService();
            Group addedGroup = service.AddGroup(group.Id, group);

            GroupController controller = new GroupController(service);

            ActionResult<DTO.Group> result = controller.CreateGroup(group.Id, new DTO.Group(group));

            Assert.AreEqual(group.Name, "Tyler");

            Group newGroup = CreateGroup();
            newGroup.Name = "Gman";

            controller.UpdateGroup(group.Id, new DTO.Group(newGroup));

            Assert.AreEqual(service.UpdateGroup_Group.Name, newGroup.Name);
        }

        /*
        [TestMethod]
        public void AddUserToGroup_Success()
        {
            TestableGroupService testService = new TestableGroupService();
            GroupController controller = new GroupController(testService);

            User user = new User
            {
                FirstName = "ted",
                LastName = "bob",
                Id = 18
            };

            Group newGroup = CreateGroup();

            controller.CreateGroup(1, new DTO.Group(newGroup));
            ActionResult<DTO.User> result = controller.AddUserToGroup(1, new DTO.User(user));

            Assert.IsTrue(result.Result is OkObjectResult);

        }
        */

        /*
        [TestMethod]
        public void RemoveUserOfGroup_Success()
        {
            TestableGroupService testService = new TestableGroupService();
            GroupController controller = new GroupController(testService);

            User user = new User
            {
                FirstName = "ted",
                LastName = "bob",
                Id = 18
            };

            Group newGroup = CreateGroup();

            controller.CreateGroup(1, new DTO.Group(newGroup));
            ActionResult<DTO.User> resultAdd = controller.AddUserToGroup(1, new DTO.User(user));
            ActionResult<DTO.User> resultRemove = controller.RemoveUserFromGroup(1, new DTO.User(user));

            Assert.AreEqual(resultAdd.Value.Id, resultAdd.Value.Id);
        }*/
    }
}