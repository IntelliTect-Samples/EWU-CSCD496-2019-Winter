using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {
        [TestInitialize]
        public async Task Initialize()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var groupService = new GroupService(context);
                var userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Billy",
                    LastName = "Bobby"
                };
                var user2 = new User
                {
                    FirstName = "Randolph",
                    LastName = "Ruby"
                };
                var user3 = new User
                {
                    FirstName = "Rinaldo",
                    LastName = "Rodney"
                };
                var user4 = new User
                {
                    FirstName = "Jackalope",
                    LastName = "John"
                };

                user = await userService.AddUser(user);
                user2 = await userService.AddUser(user2);
                user3 = await userService.AddUser(user3);
                user4 = await userService.AddUser(user4);

                var group = new Group
                {
                    Name = "Group1"
                };

                group = await groupService.AddGroup(group);

                await groupService.AddUserToGroup(group.Id, user.Id);
                await groupService.AddUserToGroup(group.Id, user2.Id);
                await groupService.AddUserToGroup(group.Id, user3.Id);
                await groupService.AddUserToGroup(group.Id, user4.Id);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_CreatesPairings()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairings(1);

                Assert.IsNotNull(pairings);
                Assert.IsTrue(pairings.Count == 4);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_SantasAreUnique()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairings(1);

                List<int> santaIds = new List<int>(); // List of uniquely found santaIds
                foreach (Pairing pairing in pairings)
                {
                    if (!santaIds.Contains(pairing.SantaId)) santaIds.Add(pairing.SantaId);
                }

                Assert.AreEqual(pairings.Count, santaIds.Count);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_RecipientsAreUnique()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairings(1);

                List<int> recipientIds = new List<int>(); // List of uniquely found recipientIds
                foreach (Pairing pairing in pairings)
                {
                    if (!recipientIds.Contains(pairing.RecipientId)) recipientIds.Add(pairing.RecipientId);
                }

                Assert.AreEqual(pairings.Count, recipientIds.Count);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_NoRecipientIsTheirOwnSanta()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairings(1);
                foreach (var pairing in pairings)
                {
                    Assert.AreNotEqual<int>(pairing.SantaId, pairing.RecipientId);
                }
            }
        }
    }
}
