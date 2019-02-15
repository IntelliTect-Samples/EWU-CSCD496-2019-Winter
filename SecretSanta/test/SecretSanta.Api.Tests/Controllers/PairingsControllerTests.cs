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
    public class PairingsControllerTests
    {
        [TestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        public async Task GeneratePairings_RequiresPositiveGroupId(int groupId)
        {
            var service = new Mock<IPairingService>(MockBehavior.Strict);
            var controller = new PairingsController(service.Object, Mapper.Instance);

            var result = await controller.GeneratePairings(groupId);

            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public async Task GeneratePairings_SuccessfullyCreatesPairingsLoop()
        {
            var pairings = new List<Pairing>
            {
                new Pairing {Id = 1, SantaId = 1, RecipientId = 2},
                new Pairing {Id = 2, SantaId = 2, RecipientId = 3},
                new Pairing {Id = 3, SantaId = 3, RecipientId = 1}
            };

            var service = new Mock<IPairingService>();
            service.Setup(x => x.GeneratePairings(It.IsAny<int>()))
                .ReturnsAsync(pairings)
                .Verifiable();

            var controller = new PairingsController(service.Object, Mapper.Instance);
            var result = await controller.GeneratePairings(1) as CreatedResult;

            var resultValue = result?.Value as List<PairingViewModel>;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual(3, resultValue.Count);
            Assert.AreEqual(1, resultValue[0].Id);
            Assert.AreEqual(1, resultValue[0].SantaId);
            Assert.AreEqual(2, resultValue[0].RecipientId);

            Assert.AreEqual(3, resultValue[2].Id);
            Assert.AreEqual(3, resultValue[2].SantaId);
            Assert.AreEqual(1, resultValue[2].RecipientId);
            service.VerifyAll();
        }
    }
}
