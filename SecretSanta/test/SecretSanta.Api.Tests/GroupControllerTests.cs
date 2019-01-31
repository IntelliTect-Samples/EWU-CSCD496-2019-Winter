using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public void CreateGroup()
        {
            var group = GroupControllerTests.GetGroup();

            var testService = new TestableGroupService();
            testService.CreateGroup(group);

            var controller = new GroupController(testService);

            ActionResult result = controller.CreateGroup(new DTO.Group(group));

            Assert.IsNotNull(result, "Returned Status Code was 200");
            Assert.AreEqual(group.Title, testService.CreateGroup_Group.Title);
        }

        [TestMethod]
        public void UpdateGroup()
        {
            var group = GetGroup();
          
            var testService = new TestableGroupService();
            testService.CreateGroup(group);
            var controller = new GroupController(testService);

            ActionResult result = controller.CreateGroup(new DTO.Group(group));

            Assert.AreEqual("Group Title", testService.CreateGroup_Group.Title);

            group.Title = "Philosophers";

            testService.UpdateGroup(group);

            result = controller.UpdateGroup(new DTO.Group(group));

            Assert.AreEqual(group.Title, testService.UpdateGroup_Group.Title);
            Assert.AreEqual(3, testService.UpdateGroup_Group.Id);
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }

        [TestMethod]
        public void DeleteGroup()
        {
            var group = GetGroup();

            var testService = new TestableGroupService();
            testService.CreateGroup(group);
            var controller = new GroupController(testService);
            ActionResult result = controller.CreateGroup(new DTO.Group(group));

            testService.DeleteGroup(group.Id);
            result = controller.DeleteGroup(group.Id);

            Assert.AreEqual(3, testService.DeleteGroup_GroupId);
            Assert.IsNotNull(result, "Returned Status Code was not 200");
        }

        [TestMethod]
        public void GroupController_GetAllGroups()
        {
            var group1 = new Group
            {
                Id = 7,
                Title = "Philosophers",
                UserGroups = new List<UserGroups>()
            };

            var group2 = new Group
            {
                Id = 8,
                Title = "Warriors",
                UserGroups = new List<UserGroups>()
            };

            var testService = new TestableGroupService
            {
                ToReturn = new List<Group>
                {
                    group1,
                    group2
                }
            };

            var controller = new GroupController(testService);

            ActionResult<List<DTO.Group>> result = controller.GetAllGroups();

            List<DTO.Group> resultGroups = result.Value;
            Assert.AreEqual("Philosophers", resultGroups[0].Title);
            Assert.AreEqual(8, resultGroups[1].Id);
            Assert.AreEqual(2, resultGroups.Count);
        }

        [TestMethod]
        public void AddUserToGroup()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Conner",
                LastName = "Verret"
            };

            var group = GetGroup();

            var testService = new TestableGroupService();
            testService.CreateGroup(group);

            var controller = new GroupController(testService);

            ActionResult result = controller.AddUserToGroup(new DTO.User(user), group.Id);
            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(3, testService.AddUserToGroup_GroupId);
            Assert.AreEqual(user.FirstName, testService.AddUserToGroup_User.FirstName);
        }

        [TestMethod]
        public void RemoveUserFromGroup()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Conner",
                LastName = "Verret"
            };

            var group = GetGroup();

            var testService = new TestableGroupService();
            testService.CreateGroup(group);

            var controller = new GroupController(testService);

            ActionResult result = controller.AddUserToGroup(new DTO.User(user), group.Id);

            Assert.IsTrue(result is OkResult);

            result = controller.RemoveUserFromGroup(user.Id, group.Id);

            Assert.IsTrue(result is OkResult);
            Assert.AreEqual(3, testService.RemoveUserFromGroup_GroupId);
            Assert.AreEqual(1, testService.RemoveUserFromGroup_UserId);
        }

        private static Domain.Models.Group GetGroup()
        {
            return new Domain.Models.Group
            {
                Id = 3,
                Title = "Group Title"
            };
        }
    }
}
