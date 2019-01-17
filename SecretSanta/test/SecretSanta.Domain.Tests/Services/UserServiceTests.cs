using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        private static User CreateUser(string fName = "Inigo", string lName = "Montoya", string giftName = "Roomba")
        {
            var user = new User
            {
                FirstName = fName,
                LastName = lName
            };

            var giftsToAdd = new List<Gift>
            {
                new Gift
                {
                    Title = giftName,
                    OrderOfImportance = 1,
                    Url = "www.someUrl.com",
                    Description = "Never vacuum again",
                    User = user
                }
            };

            user.Gifts = giftsToAdd;

            return user;
        }

        private static List<User> CreateUsers(int numberToCreate = 2)
        {
            var usersList = new List<User>();

            for (var i = 0; i < numberToCreate; i++) usersList.Add(CreateUser($"fName{i}", $"lName{i}", $"Roomba{i}"));

            return usersList;
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void UpsertUser_TestAddStaticUser_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.UpsertUser(myUser);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }
        
        [TestMethod]
        public void UpsertUser_TestUpdateUser_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var myUser = CreateUser();

                // Add user
                service.UpsertUser(myUser);
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var retrievedUser = service.Find(1);

                // Update user
                retrievedUser.LastName = "Osborn";
                service.UpsertUser(retrievedUser);
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var retrievedUser = service.Find(1);

                // Validate change
                Assert.AreEqual("Osborn", retrievedUser.LastName);
            }
        }

        [TestMethod]
        public void FindUser_TestWithStaticUser_Success()
        {
            // arrange
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var myUser = CreateUser();

                service.UpsertUser(myUser);
            }

            // act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var fetchedUser = service.Find(1);

                // assert
                Assert.AreEqual(1, fetchedUser.Id);
            }
        }

        [TestMethod]
        public void FetchUser_TestWithStaticUsers_Success()
        {
            // arrange
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var usersToAdd = CreateUsers();

                foreach (var cur in usersToAdd) service.UpsertUser(cur);
            }

            // act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new UserService(context);
                var fetchedUsers = service.FetchAll();

                // assert
                for (var i = 0; i < fetchedUsers.Count; i++) Assert.AreEqual(i + 1, fetchedUsers[i].Id);
            }
        }
    }
}