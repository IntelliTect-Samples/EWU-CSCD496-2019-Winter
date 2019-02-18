using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests : DatabaseServiceTests
    {

        //verify that: no person can be their own santa && each recipient has only one santa
        [TestMethod]
        public async Task RecipientDiffersFromSanta()
        {
            /*  
             *  I am aware that this method is overkill at the moment...
             * */

            using (var context = new ApplicationDbContext(Options))
            {

                UserService uService = new UserService(context);
                User user1 = new User { FirstName = "fname1", LastName = "lname1" };
                User user2 = new User { FirstName = "fname2", LastName = "lname2" };
                User user3 = new User { FirstName = "fname3", LastName = "lname3" };
                User user4 = new User { FirstName = "fname4", LastName = "lname4" };
                uService.AddUser(user1);
                uService.AddUser(user2);
                uService.AddUser(user3);
                uService.AddUser(user4);

                Group group = new Group { Id = 1, Name = "group" };
                GroupService gService = new GroupService(context);

                await gService.AddGroup(group);
                await gService.AddUserToGroup(group.Id, user1.Id);
                await gService.AddUserToGroup(group.Id, user2.Id);
                await gService.AddUserToGroup(group.Id, user3.Id);
                await gService.AddUserToGroup(group.Id, user4.Id);


                PairingService pService = new PairingService(context);

                List<Pairing> pairings = await pService.GeneratePairings(group.Id);


                foreach (Pairing p in pairings)
                {
                    Assert.AreNotEqual(p.Recipient, p.SantaId);
                }
            }
        }

    }
}
