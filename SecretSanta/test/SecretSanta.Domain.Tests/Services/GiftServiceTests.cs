using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class GiftServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        public void AddGift()
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

                user = userService.AddUserAsync(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                var persistedGift = giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }
        }

        [TestMethod]
        public void UpdateGift()
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

                user = userService.AddUserAsync(user);

                var gift = new Gift
                {
                    Title = "Sword",
                    OrderOfImportance = 1
                };

                var persistedGift = giftService.AddGiftToUser(user.Id, gift);

                Assert.AreNotEqual(0, persistedGift.Id);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = userService.FetchAllAsync();
                var gifts = giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);

                gifts[0].Title = "Horse";
                giftService.UpdateGiftForUser(users[0].Id, gifts[0]);                
            }

            using (var context = new ApplicationDbContext(Options))
            {
                GiftService giftService = new GiftService(context);
                UserService userService = new UserService(context);

                var users = userService.FetchAllAsync();
                var gifts = giftService.GetGiftsForUser(users[0].Id);

                Assert.IsTrue(gifts.Count > 0);
                Assert.AreEqual("Horse", gifts[0].Title);            
            }
        }
    }
}