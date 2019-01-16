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

        User CreateUser()
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
                Importance = 1,
                URL = "https://google.com",
                Description = "This gift is blue.",
                User = user,
            };

            var group = new Group
            {
                Title = "The Group",
                Users = new List<User>()
            };
            group.Users.Add(user);

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
                var user = CreateUser();

                var persistedUser = service.AddUser(user);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }
    }
}
