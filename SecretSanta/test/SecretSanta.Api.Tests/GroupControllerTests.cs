using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
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
        public void UserController_RequiresGiftService()
        {
            new GroupController(null);
        }

        [TestMethod]
        public void AddGroup_NullGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.AddGroup(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void UpdateGroup_NullGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.UpdateGroup(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        public void RemoveGroup_NullGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult result = controller.RemoveGroup(null);

            Assert.IsTrue(result is BadRequestResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FetchGroupUsers_NoValidGroupToSearch()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<List<DTO.User>> result = controller.FetchGroupUsers(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FetchAll_NoGroups()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<List<DTO.Group>> result = controller.FetchAll();
        }
    }
}
