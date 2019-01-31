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
