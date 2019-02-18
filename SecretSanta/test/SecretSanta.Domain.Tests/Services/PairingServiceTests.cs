using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests // TODO:
    {
        [TestMethod]
        public void GeneratePairing_Success()
        {
            TestableRandom testableRandom = new TestableRandom {RandomList = new List<int> {5}};
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService pairingService = new PairingService(context, testableRandom);
                UserService userService = new UserService(context);
                GroupService groupService = new GroupService(context);
                
                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                var user2 = new User
                {
                    FirstName = "Princess",
                    LastName = "Buttercup"
                };

                var user3 = new User
                {
                    FirstName = "Someone",
                    LastName = "Else"
                };
                
                List<User> users = new List<User>{user, user2, user3};
            }
        }
    }
}