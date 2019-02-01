using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.DTO;
using SecretSanta.Domain.Models;
using Group = SecretSanta.Domain.Models.Group;
using User = SecretSanta.Domain.Models.User;

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
        public void AddGroup_GroupCannotBeNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.AddGroup(null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.IsNull(testService.LastGroupModified);
        }
        
        [TestMethod]
        public void AddGroup_GroupIsAdded()
        {
            var groupBeforeAdd = CreateGroup(2);
            var testService = new TestableGroupService { GroupsToReturn = new List<Group> { groupBeforeAdd } };
            var controller = new GroupController(testService);

            var groupAfterAdd = controller.AddGroup(new DTO.Group(groupBeforeAdd)).Value;

            Assert.AreEqual(groupBeforeAdd.Id, groupAfterAdd.Id);
            Assert.AreEqual(groupBeforeAdd.Name, groupAfterAdd.Name);
            Assert.AreEqual(2, testService.LastGroupModified.Id);
        }
        
        [TestMethod]
        public void UpdateGroup_GroupCannotBeNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.UpdateGroup(null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.IsNull(testService.LastGroupModified);
        }
        
        [TestMethod]
        public void UpdateGroup_GroupIsUpdated()
        {
            var groupBeforeUpdate = CreateGroup(2);
            var testService = new TestableGroupService { GroupsToReturn = new List<Group> { groupBeforeUpdate } };
            var controller = new GroupController(testService);

            var groupAfterUpdate = controller.UpdateGroup(new DTO.Group(groupBeforeUpdate)).Value;

            Assert.AreEqual(groupBeforeUpdate.Id, groupAfterUpdate.Id);
            Assert.AreEqual(groupBeforeUpdate.Name, groupAfterUpdate.Name);
            Assert.AreEqual(2, testService.LastGroupModified.Id);
        }

        [TestMethod]
        public void DeleteGroup_GroupCannotBeNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.UpdateGroup(null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.IsNull(testService.LastGroupModified);
        }

        [TestMethod]
        public void DeleteGroup_GroupIsDeleted()
        {
            var groupBeforeRemove = CreateGroup(2);
            var testService = new TestableGroupService { GroupsToReturn = new List<Group> { groupBeforeRemove } };
            var controller = new GroupController(testService);

            var groupAfterRemove = controller.DeleteGroup(new DTO.Group(groupBeforeRemove)).Value;

            Assert.AreEqual(groupBeforeRemove.Id, groupAfterRemove.Id);
            Assert.AreEqual(groupBeforeRemove.Name, groupAfterRemove.Name);
            Assert.AreEqual(2, testService.LastGroupModified.Id);
        }

        [TestMethod]
        public void AddUserToGroup_UserCannotBeNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.AddUserToGroup(2, null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastGroupIDModified);
            Assert.IsNull(testService.LastUserModified);
        }

        [TestMethod]
        public void AddUserToGroup_GroupIdMustBePositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.AddUserToGroup(-1, new DTO.User());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastGroupIDModified);
            Assert.IsNull(testService.LastUserModified);
        }

        [TestMethod]
        public void AddUserToGroup_UserAddedToGroup()
        {
            var userBeforeAdd = CreateUser(1);
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var userAfterAdd = controller.AddUserToGroup(2, new DTO.User(userBeforeAdd)).Value;

            Assert.AreEqual(userBeforeAdd.Id, userAfterAdd.Id);
            Assert.AreEqual(userBeforeAdd.FirstName, userAfterAdd.FirstName);
            Assert.AreEqual(2, testService.LastGroupIDModified);
            Assert.AreEqual(1, testService.LastUserModified.Id);
        }

        [TestMethod]
        public void RemoveUserFromGroup_UserCannotBeNull()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.RemoveUserFromGroup(2, null);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastGroupIDModified);
            Assert.IsNull(testService.LastUserModified);
        }

        [TestMethod]
        public void RemoveUserFromGroup_GroupIdMustBePositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.RemoveUserFromGroup(-1, new DTO.User());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastGroupIDModified);
            Assert.IsNull(testService.LastUserModified);
        }

        [TestMethod]
        public void RemoveUserFromGroup_UserRemovedFromGroup()
        {
            var userBeforeRemove = CreateUser(1);
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var userAfterRemove = controller.RemoveUserFromGroup(2, new DTO.User(userBeforeRemove)).Value;

            Assert.AreEqual(userBeforeRemove.Id, userAfterRemove.Id);
            Assert.AreEqual(userBeforeRemove.FirstName, userAfterRemove.FirstName);
            Assert.AreEqual(2, testService.LastGroupIDModified);
            Assert.AreEqual(1, testService.LastUserModified.Id);
        }

        [TestMethod]
        public void QueryAllGroups_ReturnsAllGroups()
        {
            var testService = new TestableGroupService
            {
                GroupsToReturn = new List<Group> { CreateGroup(1), CreateGroup(2) }
            };
            var controller = new GroupController(testService);

            var result = controller.QueryAllGroups();
            var groups = result.Value;

            Assert.AreEqual(2, groups.Count);
            Assert.AreEqual(1, groups[0].Id);
        }

        [TestMethod]
        public void QueryAllUsersInGroup_GroupIdMustBePositive()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var result = controller.QueryAllUsersInGroup(-1);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            //This check ensures that the service was not called
            Assert.AreEqual(0, testService.LastGroupIDModified);
            Assert.IsNull(testService.LastUserModified);
        }

        [TestMethod]
        public void QueryAllUsersInGroup_UsersAreReturned()
        {
            var testService = new TestableGroupService
            {
                UsersToReturn = new List<User> { new User { Id = 2, FirstName = "Billy", LastName = "Bobby" } }
            };
            var controller = new GroupController(testService);

            var usersReturned = controller.QueryAllUsersInGroup(2).Value;

            Assert.AreEqual(1, usersReturned.Count);
            Assert.AreEqual(2, usersReturned[0].Id);
            Assert.AreEqual("Billy", usersReturned[0].FirstName);
            Assert.AreEqual(2, testService.LastGroupIDModified);
        }
        
        private static User CreateUser(int id)
        {
            return new User
            {
                Id = id,
                FirstName = "fname" + id,
                LastName = "lname" + id
            };
        }
        
        private static Group CreateGroup(int id)
        {
            return new Group
            {
                Id = id,
                Name = "group" + id
            };
        }
    }
}