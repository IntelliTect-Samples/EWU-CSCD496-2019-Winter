using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class PairingControllerTests
    {
        [TestMethod]
        public async Task GeneratePairings_ReturnsPairings()
        {
            int groupId = 1;
            List<Pairing> pairings = MockPairings(2, groupId);
            
            var service = new Mock<IPairingService>(MockBehavior.Strict);
            service.Setup(x => x.GeneratePairings(groupId))
                .ReturnsAsync(pairings)
                .Verifiable();

            var controller = new PairingController(service.Object, Mapper.Instance);

            CreatedResult result = await controller.PostGeneratePairings(groupId) as CreatedResult;

            List<PairingViewModel> resultValue = ((IEnumerable<PairingViewModel>)result.Value).ToList();
            
            Assert.AreEqual(2, resultValue.Count());
            Assert.AreEqual(1, pairings[0].SantaId);
            Assert.AreEqual(2, pairings[0].RecipientId);
            Assert.AreEqual(2, pairings[1].SantaId);
            Assert.AreEqual(1, pairings[1].RecipientId);
            
            service.VerifyAll();
        }

        [TestMethod]
        public async Task GeneratePairings_RequirePositiveNonZeroGroupId()
        {
            int groupId = 0;

            var service = new Mock<IPairingService>(MockBehavior.Strict);
            var controller = new PairingController(service.Object, Mapper.Instance);

            IActionResult result = await controller.PostGeneratePairings(groupId) as NotFoundResult;
            
            Assert.IsNotNull(result);
        }

        // stolen logic from PairingService to create Pairings to mock with
        private List<Pairing> MockPairings(int usersCount, int groupId)
        {
            List<int> userIds = new List<int>();

            for (int i = 0; i < usersCount; i++)
            {
                userIds.Add(i + 1);
            }
            
            List<Pairing> pairings = new List<Pairing>();
            
            for (var i = 0; i < userIds.Count - 1; i++)
            {
                var pairing = new Pairing
                {
                    SantaId = userIds[i],
                    RecipientId = userIds[i + 1]
                };
                pairings.Add(pairing);
            }

            var lastPairing = new Pairing
            {
                SantaId = userIds.Last(),
                RecipientId = userIds.First()
            };
            pairings.Add(lastPairing);

            return pairings;
        }
    }
}