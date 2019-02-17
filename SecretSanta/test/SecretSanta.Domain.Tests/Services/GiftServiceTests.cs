using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Collections.Generic;
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

                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                Gift persistedGift = await giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }

        [TestMethod]
        public async Task UpdateGift()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                Gift persistedGift = await giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                List<User> users = await userService.FetchAll();
                List<Gift> gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);

                gifts[0].Title = "Horse";
                Gift persistedGift = await giftService.UpdateGiftForUser(users[0].Id, gifts[0]);
                Assert.AreEqual(users[0].Id, persistedGift.UserId);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                List<User> users = await userService.FetchAll();
                List<Gift> gifts = await giftService.GetGiftsForUser(users[0].Id);

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

                User user = new User
                {
                    FirstName = "Inigo",
                    LastName = "Montoya"
                };

                user = await userService.AddUser(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                Gift persistedGift = await giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                List<User> users = await userService.FetchAll();
                List<Gift> gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                List<User> users = await userService.FetchAll();
                List<Gift> gifts = await giftService.GetGiftsForUser(users[0].Id);

                await giftService.DeleteGift(gifts[0]);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                List<User> users = await userService.FetchAll();
                List<Gift> gifts = await giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count == 0);
            }
        }
    }
}