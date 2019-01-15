using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<SecretSantaDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<SecretSantaDbContext>().UseSqlite(SqliteConnection).Options;

            using (var context = new SecretSantaDbContext(Options))
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
        public void CreateUser()
        {
            UserService userService;

            Group group1 = new Group() { Title = "Toasters", Users = new List<User>() };
            User user = new User { First = "Brad", Last = "Howard" };

            group1.Users.Add(user);

            user.Groups.Add(group1);

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                userService.UpsertUser(user);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual("Brad", user.First);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            UserService userService;

            Group group1 = new Group() { Title = "Toasters", Users = new List<User>() };
            User user = new User { First = "Brad", Last = "Howard" };

            group1.Users.Add(user);

            user.Groups.Add(group1);

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                userService.UpsertUser(user);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);
                user.First = "Toast";
                user.Last = "Maker";

                userService.UpsertUser(user);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual("Toast", user.First);
            }
        }
    }
}
