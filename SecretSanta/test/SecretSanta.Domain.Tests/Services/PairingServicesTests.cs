using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServicesTests : DatabaseServiceTests
    {
        private Group DefaultGroup { get; set; }

        [TestInitialize]
        public void StartMethod()
        {
            DefaultGroup = new Group
            {
                Name = "tempGroupName_1",
                GroupUsers = new List<GroupUser>(),
                Id = 25
            };
        }

        [TestMethod]
        public async Task GetPairingsList_ReturnListSameGroupId_NoUser_EmptyPairList()
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                GroupService groupService = new GroupService(context);

                await groupService.AddGroup(DefaultGroup);

                PairingService pairingService = new PairingService(context);

                List<Pairing> randomPairingList = await pairingService.GeneratePairing(DefaultGroup.Id);

                Assert.IsNull(randomPairingList);

                List<Pairing> pairingsFound = await pairingService.GetPairingsList(DefaultGroup.Id);

                Assert.AreEqual(0, pairingsFound.Count);
            }
        }

        [DataRow(1)]
        [TestMethod]
        public async Task GeneratePairing_ReturnListSameGroupId_OneUser_NullReturn(int userCount)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                GroupService groupService = new GroupService(context);

                List<User> testUserList = ReturnTestUserList(userCount);

                GroupService tempGroupService = new GroupService(context);

                await AddUsersToUserService(testUserList, userService);

                await groupService.AddGroup(DefaultGroup);

                await AddUsersToGroupService(testUserList, DefaultGroup, groupService);

                PairingService pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairing(DefaultGroup.Id);

                Assert.IsNull(pairings);

                List<Pairing> pairingsFound = await pairingService.GetPairingsList(DefaultGroup.Id);

                Assert.AreEqual(0, pairingsFound.Count);
            }
        }

        [DataRow(-1, 2)]
        [DataRow(0, 2)]
        [DataRow(123, 2)]
        [TestMethod]
        public async Task GeneratePairing_ReturnListSameGroupId_InvalidId_Exception(int invalidId, int userCount)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                GroupService groupService = new GroupService(context);

                List<User> testUserList = ReturnTestUserList(userCount);

                GroupService tempGroupService = new GroupService(context);

                await AddUsersToUserService(testUserList, userService);

                await groupService.AddGroup(DefaultGroup);

                await AddUsersToGroupService(testUserList, DefaultGroup, groupService);

                PairingService pairingService = new PairingService(context);
                List<Pairing> pairings = await pairingService.GeneratePairing(invalidId);

                Assert.IsNull(pairings);

                List<Pairing> pairingsFound = await pairingService.GetPairingsList(DefaultGroup.Id);

                Assert.AreEqual(0, pairingsFound.Count);
            }
        }

        [DataRow(2)]
        [DataRow(20)]
        [TestMethod]
        public async Task GeneratePairing_ReturnListSameGroupId_Even_PairList(int userCount)
        {
            using (ApplicationDbContext context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                GroupService groupService = new GroupService(context);

                List<User> testUserList = ReturnTestUserList(userCount);

                GroupService tempGroupService = new GroupService(context);

                await AddUsersToUserService(testUserList, userService);

                await groupService.AddGroup(DefaultGroup);

                await AddUsersToGroupService(testUserList, DefaultGroup, groupService);

                PairingService pairingService = new PairingService(context);
                List<Pairing> randomPairings = await pairingService.GeneratePairing(DefaultGroup.Id);

                Assert.AreEqual(userCount, randomPairings.Count);
                Assert.IsTrue(IsValidPairings(randomPairings));

                List<Pairing> pairingsFound = await pairingService.GetPairingsList(DefaultGroup.Id);
                Assert.AreEqual(userCount, pairingsFound.Count);
            }
        }

        [DataRow(3)]
        [DataRow(25)]
        [TestMethod]
        public async Task GeneratePairing_Odd_GetAllPairingsForGroup(int userCount)
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService userService = new UserService(context);
                GroupService groupService = new GroupService(context);

                List<User> testUserList = ReturnTestUserList(userCount);

                await AddUsersToUserService(testUserList, userService);

                await groupService.AddGroup(DefaultGroup);

                await AddUsersToGroupService(testUserList, DefaultGroup, groupService);

                PairingService pairingService = new PairingService(context);

                List<Pairing> randomPairings = await pairingService.GeneratePairing(DefaultGroup.Id);

                Assert.AreEqual(userCount, randomPairings.Count);
                Assert.IsTrue(IsValidPairings(randomPairings));

                List<Pairing> pairingsFound = await pairingService.GetPairingsList(DefaultGroup.Id);
                Assert.AreEqual(userCount, pairingsFound.Count);
            }
        }

        private bool IsValidPairings(List<Pairing> randomPairings)
        {
            return (IsNotOwnSanta(randomPairings) && IsNotMoreThanOneSantaOrRecp(randomPairings));
        }

        private bool IsNotOwnSanta(List<Pairing> pairingList)
        {
            foreach (Pairing pair in pairingList)
            {
                if (pair.Recipient == pair.Santa)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsNotMoreThanOneSantaOrRecp(List<Pairing> pairingList)
        {
            int roleCount = 0;
            foreach (Pairing pair in pairingList)
            {
                foreach(Pairing comparePair in pairingList)
                {
                    if (pair.SantaId == comparePair.RecipientId)
                    {
                        roleCount++;
                    }
                }
                if (roleCount != 1)
                {
                    return false;
                }
                roleCount = 0;
            }
            return true;
        }

        private List<User> ReturnTestUserList(int userAmount)
        {
            List<User> madeUserList = new List<User>();

            if (userAmount < 0)
            {
                return madeUserList;
            }

            for (int i = 0; i < userAmount; i++)
            {
                madeUserList.Add(new User
                {
                    FirstName = $"tempUserFirstName_{i+1}",
                    LastName = $"tempUserLastName_{i+1}",
                    Id = i + 1,
                    Gifts = new List<Gift>(),
                    GroupUsers = new List<GroupUser>()
                });
            }
            return madeUserList;
        }

        private async Task AddUsersToUserService(List<User> userList, UserService userService)
        {
            foreach (User user in userList)
            {
                await userService.AddUser(user);
            }
        }

        private async Task AddUsersToGroupService(List<User> userList, Group group, GroupService groupService)
        {
            foreach (User user in userList)
            {
                await groupService.AddUserToGroup(group.Id, user.Id);
            }
        }
    }
}