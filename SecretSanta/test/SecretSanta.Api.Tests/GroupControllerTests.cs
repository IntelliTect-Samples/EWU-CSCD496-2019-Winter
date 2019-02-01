using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupController_RequiresGroupService()
        {
            var groupController = new GroupController(null);
        }

        [TestMethod]
        public void CreateGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.Group> result = controller.CreateGroup(null);
            
            Assert.IsTrue(result.Result is BadRequestResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.AddGroup_Group);
        }

        [TestMethod]
        public void CreateGroup_InvokesService()
        {
            var group = new Domain.Models.Group
            {
                Id = 12,
                Name = "Test Group"
            };

            var testService = new TestableGroupService { AddGroup_Return = group };
            var controller = new GroupController(testService);

            var returnedAction = controller.CreateGroup(new DTO.Group(group)).Result;
            
            Assert.IsTrue(returnedAction is OkObjectResult);
            
            OkObjectResult result = returnedAction as OkObjectResult;
            
            Assert.AreEqual(group.Id, testService.AddGroup_Group.Id);

            var valueOfResult = result.Value as DTO.Group;
            
            Assert.IsNotNull(valueOfResult);
            Assert.AreEqual(group.Id, valueOfResult.Id);
        }
        
        [TestMethod]
        public void UpdateGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.Group> result = controller.UpdateGroup(null);
            
            Assert.IsTrue(result.Result is BadRequestResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.UpdateGroup_Group);
        }

        [TestMethod]
        public void UpdateGroup_InvokesService()
        {
            var group = new Domain.Models.Group
            {
                Id = 12,
                Name = "Test Group"
            };

            var testService = new TestableGroupService { UpdateGroup_Return = group };
            var controller = new GroupController(testService);

            var returnedAction = controller.UpdateGroup(new DTO.Group(group)).Result;
            
            Assert.IsTrue(returnedAction is OkObjectResult);
            
            OkObjectResult result = returnedAction as OkObjectResult;
            
            Assert.AreEqual(group.Id, testService.UpdateGroup_Group.Id);

            var valueOfResult = result.Value as DTO.Group;
            
            Assert.IsNotNull(valueOfResult);
            Assert.AreEqual(group.Id, valueOfResult.Id);
        }
        
        [TestMethod]
        public void DeleteGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.Group> result = controller.DeleteGroup(null);
            
            Assert.IsTrue(result.Result is BadRequestResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.DeleteGroup_Group);
        }
        
        [TestMethod]
        public void DeleteGroup_InvokesService()
        {
            var group = new Domain.Models.Group
            {
                Id = 12,
                Name = "Test Group"
            };

            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            var returnedAction = controller.DeleteGroup(new DTO.Group(group));
            
            Assert.IsTrue(returnedAction is OkResult);
            
            // Ensure service was called
            Assert.AreEqual(group.Id, testService.DeleteGroup_Group.Id);
        }

        [TestMethod]
        public void AddUserToGroup_RequiresPositiveGroupId()
        {
            var user = new Domain.Models.User
            {
                Id = 12,
                FirstName = "Kevin",
                LastName = "Bost"
            };
            
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.User> result = controller.AddUserToGroup(-1, new DTO.User(user));
            
            Assert.IsTrue(result.Result is NotFoundObjectResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.AddUserToGroup_User);
        }
        
        [TestMethod]
        public void AddUserToGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.User> result = controller.AddUserToGroup(4, null);
            
            Assert.IsTrue(result.Result is BadRequestResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.AddGroup_Group);
        }
        
        [TestMethod]
        public void AddUserToGroup_InvokesService()
        {
            var user = new Domain.Models.User
            {
                Id = 12,
                FirstName = "Kevin",
                LastName = "Bost"
            };

            var testService = new TestableGroupService { AddUserToGroup_Return = user };
            var controller = new GroupController(testService);

            var returnedAction = controller.AddUserToGroup(1, new DTO.User(user)).Result;
            
            Assert.IsTrue(returnedAction is OkObjectResult);
            
            OkObjectResult result = returnedAction as OkObjectResult;
            
            Assert.AreEqual(user.Id, testService.AddUserToGroup_User.Id);

            var valueOfResult = result.Value as DTO.User;
            
            Assert.IsNotNull(valueOfResult);
            Assert.AreEqual(user.Id, valueOfResult.Id);
        }
        
        [TestMethod]
        public void RemoveUserFromGroup_RequiresPositiveGroupId()
        {
            var user = new Domain.Models.User
            {
                Id = 12,
                FirstName = "Kevin",
                LastName = "Bost"
            };
            
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.User> result = controller.RemoveUserFromGroup(-1, new DTO.User(user));
            
            Assert.IsTrue(result.Result is NotFoundObjectResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.RemoveUserFromGroup_User);
        }
        
        [TestMethod]
        public void RemoveUserFromGroup_RequiresGroup()
        {
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<DTO.User> result = controller.RemoveUserFromGroup(4, null);
            
            Assert.IsTrue(result.Result is BadRequestResult);
            
            // This check ensures that the service was not called
            Assert.IsNull(testService.RemoveUserFromGroup_User);
        }
        
        [TestMethod]
        public void RemoveUserFromGroup_InvokesService() // TODO:
        {
            var user = new Domain.Models.User
            {
                Id = 12,
                FirstName = "Kevin",
                LastName = "Bost"
            };

            var testService = new TestableGroupService { RemoveUserFromGroup_Return = user };
            var controller = new GroupController(testService);

            var returnedAction = controller.RemoveUserFromGroup(1, new DTO.User(user)).Result;
            
            Assert.IsTrue(returnedAction is OkObjectResult);
            
            OkObjectResult result = returnedAction as OkObjectResult;
            
            Assert.AreEqual(user.Id, testService.RemoveUserFromGroup_User.Id);

            var valueOfResult = result.Value as DTO.User;
            
            Assert.IsNotNull(valueOfResult);
            Assert.AreEqual(user.Id, valueOfResult.Id);
        }
        
        [TestMethod]
        public void FetchAll_InvokesService()
        {
            var group = new Domain.Models.Group
            {
                Id = 12,
                Name = "Test Group"
            };
            
            List<Group> toReturn  = new List<Group>{group};
            
            var testService = new TestableGroupService{ FetchAll_Return = toReturn };
            var controller = new GroupController(testService);

            ActionResult<List<DTO.Group>> result = controller.FetchAll();
            
            Assert.IsNotNull(result);
            
            // Ensure service was called
            Assert.IsNotNull(result.Value);
        }
        
        [TestMethod]
        public void GetUsers_RequiresPositiveGroupId()
        {      
            var testService = new TestableGroupService();
            var controller = new GroupController(testService);

            ActionResult<List<DTO.User>> result = controller.GetUsers(-1);
            
            Assert.IsTrue(result.Result is NotFoundObjectResult);
            
            // This check ensures that the service was not called
            Assert.AreEqual(0, testService.GetUsers_GroupId);
        }
        
        [TestMethod]
        public void GetUsers_InvokesService()
        {
            var user = new Domain.Models.User
            {
                Id = 1,
                FirstName = "Kevin",
                LastName = "Bost"
            };

            List<Domain.Models.User> toReturn = new List<Domain.Models.User> {user};
            
            var testService = new TestableGroupService{ GetUsers_Return = toReturn };
            var controller = new GroupController(testService);

            ActionResult<List<DTO.User>> result = controller.GetUsers(1);
            
            Assert.IsNotNull(result);
            
            // Ensure service was called
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(1, testService.GetUsers_GroupId);
        }
    }
}