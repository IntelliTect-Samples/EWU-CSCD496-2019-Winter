using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingTest : ApplicationServiceTest
    {
        public static Group CreateGroup()
        {
            var user1 = new User { FirstName = "Inigo" };
            var user2 = new User { FirstName = "Princess" };
            var user3 = new User { FirstName = "Westley" };
            var user4 = new User { FirstName = "Vizzini" };
            User[] users = { user1, user2, user3, user4 };

            Group group = new Group { Title = "test group", Members = new List<User>()};

            foreach (User u in users)
            {
                group.Members.Add(u);
                //u.UserGroups.Add(group);
            }
            return group;
        }

        [TestMethod]
        public void CreatePairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService service = new PairingService(context);

                var group = CreateGroup();
                var pairing = service.CreatePairing(group);

                Assert.AreNotEqual(pairing.Recipient, pairing.Santa);
            }
        }
       

        [TestMethod] 
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PairingService service = new PairingService(context);

                var group = CreateGroup();
                var pairing = service.CreatePairing(group);
                var persistedPairing = service.AddPairing(pairing);

                Assert.AreNotEqual(pairing.Recipient, pairing.Santa);
                Assert.AreNotEqual(0, persistedPairing.Id);
            }
        }
    
    }
}
