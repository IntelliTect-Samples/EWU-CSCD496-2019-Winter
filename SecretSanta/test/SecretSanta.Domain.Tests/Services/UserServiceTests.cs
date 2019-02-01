using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class UserServiceTests : DatabaseServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UserService_RequiresDbContext()
        {
            new UserService(null);
        }

        [TestMethod]
        public void AddUser_PersistsUser()
        {
            var user = new User
            {
                FirstName = "Kevin",
                LastName = "Bost"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);

                User addedUser = service.AddUser(user);
                Assert.AreEqual(addedUser, user);
                Assert.AreNotEqual(0, addedUser.Id);
            }

            //Nice test.
            using (var context = new ApplicationDbContext(Options))
            {
                User retrievedUser = context.Users.Single();
                Assert.AreEqual(user.FirstName, retrievedUser.FirstName);
                Assert.AreEqual(user.LastName, retrievedUser.LastName);
            }
        }

        [TestMethod]
        public void UpdateUser_UpdatesExistingUser()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };
            using (var context = new ApplicationDbContext(Options))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            user.FirstName = "Jane";
            user.LastName = "Other";
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                User updatedUser = service.UpdateUser(user);
                Assert.AreEqual(user, updatedUser);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                User retrievedUser = context.Users.Single();
                Assert.AreEqual(user.Id, retrievedUser.Id);
                Assert.AreEqual(user.FirstName, retrievedUser.FirstName);
                Assert.AreEqual(user.LastName, retrievedUser.LastName);
            }
        }
    }
}