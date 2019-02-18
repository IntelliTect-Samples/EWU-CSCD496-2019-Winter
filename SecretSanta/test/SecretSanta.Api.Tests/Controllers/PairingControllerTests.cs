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

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests
    {
        [AssemblyInitialize]
        public static void ConfigureAutoMapper(TestContext context)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
        }
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();

        }


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





        }

        [TestMethod]
        public async Task GeneratePairings_InvalidId()
        {
            //invalid group
        }

        [TestMethod]
        public async Task GeneratePairings_GroupDoesNotContainMembers()
        {
            //group doesn't contain members
        }
    }
}
