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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests 
    {
        [TestMethod]
        public async Task BuildPairingForAGroup_Ok()
        {
            Group group = new Group()
            {
                Id = 1,
                Name = "The Titans"
            };

            Mock<IPairingService> service = new Mock<IPairingService>();
            service.Setup(x => x.GenerateAllPairs(group.Id)).ReturnsAsync(true).Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            OkResult result = await controller.MakePairings(group.Id) as OkResult;
            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task BuildPairingForAGroup_BadRequest_GroupNotFound()
        {
            Group group = new Group()
            {
                Id = 1,
                Name = "The Titans"
            };

            Mock<IPairingService> service = new Mock<IPairingService>();
            service.Setup(x => x.GenerateAllPairs(group.Id)).ReturnsAsync(true).Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            BadRequestResult result = await controller.MakePairings(group.Id + 1) as BadRequestResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task BuildPairingForAGroup_BadRequest_DueToTooFew()
        {
            Mock<IPairingService> service = new Mock<IPairingService>(MockBehavior.Strict);
            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            BadRequestObjectResult result = await controller.MakePairings(0) as BadRequestObjectResult;

            Assert.IsNotNull(result);
        }
    }
}
