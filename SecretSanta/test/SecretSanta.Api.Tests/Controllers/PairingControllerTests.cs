using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests
    { 

        [TestMethod]
        public async Task InvalidGroupID()
        {
            var service = new Mock<IPairingService>(MockBehavior.Strict);
            var controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.GenerateUserPairings(0);
            Assert.IsTrue(result is BadRequestResult);

        }

        [TestMethod]
        public async Task TestASuccessfulCreationOfPairings()
        {
            Pairing pairing1 = new Pairing { Id = 1, RecipientId = 1, SantaId = 2 };
            Pairing pairing2 = new Pairing { Id = 2, RecipientId = 2, SantaId = 3 };
            Pairing pairing3 = new Pairing { Id = 3, RecipientId = 3, SantaId = 1 };

            List<Pairing> listOfPairing = new List<Pairing>();
            listOfPairing.Add(pairing1);
            listOfPairing.Add(pairing2);
            listOfPairing.Add(pairing3);

            var service = new Mock<IPairingService>();
            service.Setup(x => x.GenerateUserPairings(It.IsAny<int>()))
                .ReturnsAsync(listOfPairing)
                .Verifiable();

            var controller = new PairingController(service.Object, Mapper.Instance);
            var result = await controller.GenerateUserPairings(1);

            Assert.IsTrue(result is CreatedResult);

        }
    }
}
