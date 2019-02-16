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
        public async Task GeneratePairingsForGroup_OneUserInGroup_ReturnsNull()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var userService = new UserService(context);
                var groupService = new GroupService(context);

                User user1 = new User
                {
                    FirstName = "Conner",
                    LastName = "Verret"
                };

                Group group1 = new Group
                {
                    Name = "Philosophers",
                    GroupUsers = new List<GroupUser>()
                };

                await userService.AddUser(user1);
                await groupService.AddGroup(group1);
                await groupService.AddUserToGroup(1, 1);

                var pairingService = new PairingService(context);

                List<Pairing> pairings = await pairingService.GeneratePairingsForGroup(1);

                Assert.IsNull(pairings);
            }
        }

        [TestMethod]
        public async Task GeneratePairings_SuccessfullyWithEvenNumberOfUsers()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var userService = new UserService(context);
                var groupService = new GroupService(context);

                User user1 = new User
                {
                    FirstName = "Conner",
                    LastName = "Verret"
                };

                User user2 = new User
                {
                    FirstName = "Alan",
                    LastName = "Watts"
                };

                User user3 = new User
                {
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger"
                };

                User user4 = new User
                {
                    FirstName = "Barack",
                    LastName = "Obama"
                };

                await userService.AddUser(user1);
                await userService.AddUser(user2);
                await userService.AddUser(user3);
                await userService.AddUser(user4);

                Group group = new Group
                {
                    Name = "Philosophers",
                    GroupUsers = new List<GroupUser>()
                };

                await groupService.AddGroup(group);
                await groupService.AddUserToGroup(group.Id, user1.Id);
                await groupService.AddUserToGroup(group.Id, user2.Id);
                await groupService.AddUserToGroup(group.Id, user3.Id);
                await groupService.AddUserToGroup(group.Id, user4.Id);

                var pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairingsForGroup(1);

                Assert.IsTrue(pairings.Count == 4);
            }
        }
    }
}
