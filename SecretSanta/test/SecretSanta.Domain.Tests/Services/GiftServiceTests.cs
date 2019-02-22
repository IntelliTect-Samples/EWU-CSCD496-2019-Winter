using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GiftServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public async Task AddGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1,
                    UserId = user.Id
                };

                var persistedGift = await giftService.AddGift(gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }

        [TestMethod]
        public async Task FetchGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1,
                    UserId = user.Id
                };

                var persistedGift = await giftService.AddGift(gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                var retrievedGift = await giftService.GetGift(1);

                Assert.AreEqual("Sword", retrievedGift.Title);
            }
        }

        [TestMethod]
        public async Task UpdateGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1,
                    UserId = user.Id
                };

                var persistedGift = await giftService.AddGift(gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = await userService.FetchAll();
                var gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);

                gifts[0].Title = "Horse";
                await giftService.UpdateGift(gifts[0]);                
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = await userService.FetchAll();
                var gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);
                Assert.AreEqual("Horse", gifts[0].Title);            
            }
        }

        [TestMethod]
        public async Task DeleteGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1,
                    UserId = user.Id
                };

                var persistedGift = await giftService.AddGift(gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);

                await giftService.RemoveGift(1);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = await userService.FetchAll();
                var gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count == 0);
            }
        }
    }
}