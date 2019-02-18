using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public async Task PairingService_GeneratePairings_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var temp = await SeedDatabase(3);
                var pairingService = new PairingService(context);
                var result = await pairingService.GeneratePairings(temp.groupId);
                
                Assert.AreEqual(3, result.Count);
                // Ensure that a loop is created
                Assert.AreEqual(result[0].SantaId, result[2].RecipientId);
            }
        }
       
        [TestMethod]
        public async Task PairingService_PairingsAreRandom()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var temp = await SeedDatabase(5);
                
                TestableRandom testableRandom = new TestableRandom
                {
                    RandomList = new List<int>{5, 1, 4, 3, 2, 9, 2, 1, 2, 4}
                };
                
                var pairingService = new PairingService(context, testableRandom);

                var result = await pairingService.GeneratePairings(temp.groupId);
                var result2 = await pairingService.GeneratePairings(temp.groupId);

                Assert.IsFalse(result.SequenceEqual(result2));
            }
        }
        
        [TestMethod]
        public async Task PairingService_PairingsDifferOnlyByGroup()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                // Manually seeding database here to put same users in multiple groups
                var userService = new UserService(context);
                var groupService = new GroupService(context);
                
                var user1 = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                var user2 = new User
                {
                    FirstName = "Princess",
                    LastName = "ButterCup"
                };

                await userService.AddUser(user1);
                await userService.AddUser(user2);

                var group1 = new Group
                {
                    Name = "Group1"
                };

                var group2 = new Group
                {
                    Name = "Group2"
                };

                await groupService.AddGroup(group1);
                await groupService.AddGroup(group2);

                await groupService.AddUserToGroup(group1.Id, user1.Id);
                await groupService.AddUserToGroup(group1.Id, user2.Id);
                await groupService.AddUserToGroup(group2.Id, user1.Id);
                await groupService.AddUserToGroup(group2.Id, user2.Id);
                
                
                TestableRandom testableRandom = new TestableRandom
                {
                    RandomList = new List<int>{5, 1, 5, 1}
                };
                
                var pairingService = new PairingService(context, testableRandom);

                var result = await pairingService.GeneratePairings(group1.Id);
                var result2 = await pairingService.GeneratePairings(group2.Id);

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(result[i].SantaId, result2[i].SantaId);
                }
            }
        }

        [TestMethod]
        public async Task PairingService_NotMultipleLoops()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                int userIds = 12;
                var temp = await SeedDatabase(userIds);
                var pairingService = new PairingService(context);
                var result = await pairingService.GeneratePairings(temp.groupId); // let this be truly random

                int uniqueSantaIds = result.Select(pairing => pairing.SantaId).Distinct().Count();
                int uniqueRecipientIds = result.Select(pairing => pairing.RecipientId).Distinct().Count();
                
                Assert.AreEqual(userIds, uniqueSantaIds);
                Assert.AreEqual(userIds, uniqueRecipientIds);
            }
        }

        [TestMethod]
        public async Task PairingService_EveryPersonIsSantaForExactlyOneOther()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                int userIds = 3;
                var temp = await SeedDatabase(userIds);
                var pairingService = new PairingService(context);
                var result = await pairingService.GeneratePairings(temp.groupId);

                int distinctSantas = result.Select(pairing => pairing.SantaId).Distinct().Count();
                
                Assert.AreEqual(userIds, distinctSantas);
            }
        }
        
        [TestMethod]
        public async Task PairingService_EveryPersonIsRecipientOfOneOther()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                int userIds = 3;
                var temp = await SeedDatabase(userIds);
                var pairingService = new PairingService(context);
                var result = await pairingService.GeneratePairings(temp.groupId);

                int distinctRecipients = result.Select(pairing => pairing.RecipientId).Distinct().Count();
                
                Assert.AreEqual(userIds, distinctRecipients);
            }
        }
        
        [TestMethod]
        public async Task PairingService_NoPersonIsOwnSanta() // Technically this is checked by a couple other tests
        {
            using (var context = new ApplicationDbContext(Options))
            {
                int userIds = 3;
                var temp = await SeedDatabase(userIds);
                var pairingService = new PairingService(context);
                var result = await pairingService.GeneratePairings(temp.groupId);

                foreach (Pairing cur in result)
                {
                    Assert.AreNotEqual(cur.SantaId, cur.RecipientId);
                }
            }
        }

        private static int _seededGroupCount = 0; // ensures that the users/group name are unique. Keeps count of groups seeded
        private async Task<(List<User> addedUsers, int groupId)> SeedDatabase(int userCount)
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var userService = new UserService(context);
                
                List<User> usersAdded = new List<User>();

                for (int i = 0; i < userCount; i++)
                {
                    User user = new User
                    {
                        FirstName = $"User{i}-{++_seededGroupCount}",
                        LastName = $"User{i}-{++_seededGroupCount}"
                    };
                    usersAdded.Add(user);
                    await userService.AddUser(user);
                }
                
                var group = new Group
                {
                    Name = $"Group-{++_seededGroupCount}"
                };
                
                var groupService = new GroupService(context);
                Group addedGroup = await groupService.AddGroup(group);

                foreach (var curUser in usersAdded)
                {
                    await groupService.AddUserToGroup(addedGroup.Id, curUser.Id);
                }

                return (usersAdded, group.Id);
            }
        }
    }
}