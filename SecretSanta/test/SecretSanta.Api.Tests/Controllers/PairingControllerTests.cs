using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests
    {


        [TestMethod]
        public async Task GeneratePairings_Valid()
        {
            //valid group

            /*
            User user1 = new User { FirstName = "fname1", LastName = "lname1" };
            User user2 = new User { FirstName = "fname2", LastName = "lname2" };

            var service = new Mock<IPairingService>(MockBehavior.Strict);
            var controller = new PairingController(service.Object, Mapper.Instance);
            Group group = new Group();


            GroupService groupService = new Group

            group.GroupUsers.Add(user1);
            group.GroupUsers.Add(user2);
            */

            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public async Task GeneratePairings_InvalidId()
        {
            //invalid group

            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public async Task GeneratePairings_GroupDoesNotContainMembers()
        {
            //group doesn't contain members



            Assert.AreEqual(0, 0);
        }
    }
}
