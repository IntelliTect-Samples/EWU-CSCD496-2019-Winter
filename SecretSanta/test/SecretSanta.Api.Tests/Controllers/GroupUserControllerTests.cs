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
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GroupUserControllerTests
    {
        [TestMethod]
        public async Task GetUsersFromGroup_Ok()
        {
            Group group = new Group
            {
                Id = 1,
                Name = "The Titans",
                GroupUsers = new List<GroupUser>()
            };

            User user1 = new User
            {
                FirstName = "Fox",
                LastName = "Hau"
            };

            User user2 = new User
            {
                FirstName = "Rena",
                LastName = "Hau"
            };

            group.GroupUsers.Add(new GroupUser { GroupId = group.Id, UserId = user1.Id });
            group.GroupUsers.Add(new GroupUser { GroupId = group.Id, UserId = user2.Id });

            Mock<IGroupService> service = new Mock<IGroupService>();
            service.Setup(x => x.GetById(1)).ReturnsAsync(group).Verifiable();

            GroupsController controller = new GroupsController(service.Object, Mapper.Instance);

            OkObjectResult result = await controller.GetGroup(1) as OkObjectResult;

            GroupViewModel resultValue = (GroupViewModel)result.Value;

            Assert.AreEqual(group.Id, resultValue.Id);

            service.VerifyAll();
        }
    }
}
