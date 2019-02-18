using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task GeneratePairings_ReturnRandomPairingsList_OKResult()
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(3);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GeneratePairing(1))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GeneratePairings(1);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.IsTrue(okObjectResult is OkObjectResult);
        }

        [TestMethod]
        public async Task GeneratePairings_ReturnRandomPairingsListList_NotFoundResult()
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(2);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GeneratePairing(13))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GeneratePairings(14);

            NotFoundResult notFoundResult = result as NotFoundResult;

            Assert.IsTrue(notFoundResult is NotFoundResult);

        }


        [DataRow(-1)]
        [DataRow(0)]
        [TestMethod]
        public async Task GeneratePairings_ReturnRandomPairingsList_BadRequest(int invalidGroupId)
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(3);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GeneratePairing(invalidGroupId))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GeneratePairings(invalidGroupId);

            BadRequestResult badRequestResult = result as BadRequestResult;

            Assert.IsTrue(badRequestResult is BadRequestResult);
        }



        [TestMethod]
        public async Task GetPairings_ReturnPairingsList_OKResult()
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(3);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GetPairingsList(1))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GetPairingList(1);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.IsTrue(okObjectResult is OkObjectResult);
        }

        [TestMethod]
        public async  Task GetPairings_ReturnPairingsList_NotFoundResult()
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(2);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GetPairingsList(13))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GetPairingList(14);

            NotFoundResult notFoundResult = result as NotFoundResult;

            Assert.IsTrue(notFoundResult is NotFoundResult);

        }


        [DataRow(-1)]
        [DataRow(0)]
        [TestMethod]
        public async Task GetPairings_ReturnPairingsList_BadRequest(int invalidGroupId)
        {
            HttpClient client = Factory.CreateClient();

            List<Pairing> pairings = GetTestPairings(3);

            Mock<IPairingService> service = new Mock<IPairingService>();

            service.Setup(x => x.GetPairingsList(invalidGroupId))
                .ReturnsAsync(pairings)
                .Verifiable();

            PairingController controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GetPairingList(invalidGroupId);

            BadRequestResult badRequestResult = result as BadRequestResult;

            Assert.IsTrue(badRequestResult is BadRequestResult);
        }


        private List<Pairing> GetTestPairings(int numUsers)
        {
            List<Pairing> pairings = new List<Pairing>();

            List<int> userIds = new List<int>();
            for (int i = 1; i < numUsers; i++)
            {
                userIds.Add(i);
            }

            for (int i = 0; i < userIds.Count - 1; i++)
            {
                Pairing pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i],
                    GroupId = 1
                };

                pairings.Add(pairing);
            }

            Pairing lastPairing = new Pairing
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
