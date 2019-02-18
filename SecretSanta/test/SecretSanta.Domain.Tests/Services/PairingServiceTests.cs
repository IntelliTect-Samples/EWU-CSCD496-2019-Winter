using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {
        [TestInitialize]
        public void Initialize()
        {
            SeedDatabase(3);
        }

        [TestMethod]
        public async Task PairingService_GeneratePairings_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var pairingService = new PairingService(context);
                TestableRandom random = new TestableRandom { RandomList = new List<int>{5}};
                var result = await pairingService.GeneratePairings(1);
                
                Assert.AreEqual(3, result.Count);
                // Ensure that a loop is created
                Assert.AreEqual(result[0].SantaId, result[2].RecipientId);
            }
        }

        private static int _groupCount = 0; // ensures that the users/group name are unique. Keeps count of groups seeded
        private async void SeedDatabase(int userCount)
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var userService = new UserService(context);
                
                List<User> usersAdded = new List<User>();

                for (int i = 0; i < userCount; i++)
                {
                    User user = new User
                    {
                        FirstName = $"User{i}-{++_groupCount}",
                        LastName = $"User{i}-{++_groupCount}"
                    };
                    usersAdded.Add(user);
                    await userService.AddUser(user);
                }
                
                var group = new Group
                {
                    Name = $"Group-{_groupCount}"
                };
                
                var groupService = new GroupService(context);
                Group addedGroup = await groupService.AddGroup(group);

                foreach (var curUser in usersAdded)
                {
                    await groupService.AddUserToGroup(addedGroup.Id, curUser.Id);
                }
            }
        }
    }
}