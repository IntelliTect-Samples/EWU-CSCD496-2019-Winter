using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public async Task MakePairs_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GroupService groupService = new GroupService(context);
                UserService userService = new UserService(context);
                PairingService pairingService = new PairingService(context);

                User user1 = new User { FirstName = "Brad", LastName = "Howard" };
                User user2 = new User { FirstName = "Rena", LastName = "Hau" };
                User user3 = new User { FirstName = "Miles", LastName = "Prower" };
                User user4 = new User { FirstName = "Big", LastName = "Bob" };

                user1 = await userService.AddUser(user1);
                user2 = await userService.AddUser(user2);
                user3 = await userService.AddUser(user3);
                user4 = await userService.AddUser(user4);

                Group group = new Group { Name = "The Titans" };

                group = await groupService.AddGroup(group);

                int i = 1;

                while (i != 5)
                {
                    await groupService.AddUserToGroup(1, i);
                    i++;
                }

                bool result = await pairingService.GenerateAllPairs(1);

                Assert.IsTrue(result);
                Assert.AreEqual(4, context.Pairings.Local.Count);
            }
        }

        [TestMethod]
        public async Task MakePairs_NullCheck()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GroupService groupService = new GroupService(context);
                UserService userService = new UserService(context);
                PairingService pairingService = new PairingService(context);

                bool result = await pairingService.GenerateAllPairs(1);

                Assert.IsFalse(result);
                Assert.AreEqual(0, context.Pairings.Local.Count);
            }
        }

        [TestMethod]
        public async Task MakePairs_InsufficientUsers()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GroupService groupService = new GroupService(context);
                UserService userService = new UserService(context);
                PairingService pairingService = new PairingService(context);

                User user1 = new User { FirstName = "Brad", LastName = "Howard" };

                user1 = await userService.AddUser(user1);

                Group group = new Group { Name = "The Titans" };

                group = await groupService.AddGroup(group);

                await groupService.AddUserToGroup(1, 1);

                bool result = await pairingService.GenerateAllPairs(1);

                Assert.IsFalse(result);
                Assert.AreEqual(0, context.Pairings.Local.Count);
            }
        }
    }
}
