using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
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
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GeneratePairings_ReturnsPairingsFromService()
        {
            var client = Factory.CreateClient();

            var pairings = GetTestPairings();

            var service = new Mock<IPairingService>();
            service.Setup(x => x.GeneratePairingsForGroup(1))
                .ReturnsAsync(pairings)
                .Verifiable();

            var controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GeneratePairings(1);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.IsTrue(okObjectResult is OkObjectResult);
        }

        [TestMethod]
        public async Task GetPairings_ReturnsPairingsFromService()
        {
            var client = Factory.CreateClient();

            var pairings = GetTestPairings();

            var service = new Mock<IPairingService>();
            service.Setup(x => x.GetPairingsForGroup(1))
                .ReturnsAsync(pairings)
                .Verifiable();

            var controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GetPairingsForGroup(1);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.IsTrue(okObjectResult is OkObjectResult);
        }

        private List<Pairing> GetTestPairings()
        {
            List<Pairing> pairings = new List<Pairing>();

            List<int> userIds = new List<int>();
            for(int i = 1; i < 5; i++)
            {
                userIds.Add(i);
            }

            for(int i = 0; i < userIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i],
                    GroupId = 1
                };

                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First(),
                GroupId = 1
            };

            pairings.Add(lastPairing);

            return pairings;
        }
    }
}
