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

        private User CreateUser()
        {
            return new User
            {
                FirstName = "Conner",
                LastName = "Verret"
            };
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
        public void AddUser()
        {
            UserService userService;
            User user = CreateUser();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                userService.CreateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                user = userService.Find(1);
                Assert.AreEqual("Conner", user.FirstName);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            UserService userService;
            User user = CreateUser();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                userService.CreateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);
                user.FirstName = "Princess";
                user.LastName = "Buttercup";

                userService.UpdateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                user = userService.Find(1);

                Assert.AreEqual("Princess", user.FirstName);
            }
        }

        [TestMethod]
        public void DeleteUser()
        {
            UserService userService;
            User user = CreateUser();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                userService.CreateUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                userService.DeleteUser(user);
                Assert.IsNull(userService.Find(user.Id));
            }
        }

        [TestMethod]
        public void FindUser()
        {
            UserService userService;
            User user = CreateUser();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                userService.UpsertUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual("Conner", user.FirstName);
            }
        }
    }
}
