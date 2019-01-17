using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

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

        private User CreateUser()
        {
            var user = new User
            {
                FirstName = "Billy",
                LastName = "Bobby",
                Gifts = new List<Gift>(),
                Groups = new List<Group>()
            };

            var gift = new Gift
            {
                Title = "My Gift",
                OrderOfImportance = 1,
                Url = "store.com",
                Description = "This gift is blue.",
                User = user,
            };

            var group = new Group
            {
                Title = "The Group",
                //Users = new List<User>()
            };
            //group.Users.Add(user);

            user.Gifts.Add(gift);
            user.Groups.Add(group);

            return user;
        }

        [TestMethod]
        public void AddUser()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(dbContext);
                User user = CreateUser();

                service.AddUser(user);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(dbContext);
                var fetchedUser = service.Find(1);

                Assert.AreEqual("Billy", fetchedUser.FirstName);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(dbContext);
                User user = CreateUser();

                service.AddUser(user);

                user.FirstName = "Update FirstName";
                service.UpdateUser(user);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(dbContext);
                var fetchedUser = service.Find(1);

                Assert.AreEqual("Update FirstName", fetchedUser.FirstName);
            }
        }        
    }
}
