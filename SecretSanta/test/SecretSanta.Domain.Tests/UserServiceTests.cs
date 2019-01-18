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
            GroupService groupService;

            Group group = new Group() { Title = "Toasters", UserGroups = new List<UserGroup>() };
            User user = new User() { First = "Brad", Last = "Howard" };
            UserGroup userGroup;

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);
                groupService = new GroupService(context);

                userService.UpsertUser(user);
                groupService.CreateGroup(group.Title);

                userGroup = new UserGroup() { Group = groupService.FindGroup(1), GroupId = 1, User = userService.Find(1), UserId = 1 };

                user = userService.Find(1);
                user.UserGroups.Add(userGroup);
                userService.UpsertUser(user);

                group = groupService.FindGroup(1);
                group.UserGroups.Add(userGroup);
                groupService.UpdateGroup(group);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);
                groupService = new GroupService(context);

                user = userService.Find(1);

                Assert.AreEqual<string>("Brad", user.First);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            UserService userService;
            GroupService groupService;

            Group group = new Group() { Title = "Toasters", UserGroups = new List<UserGroup>() };
            User user = new User() { First = "Brad", Last = "Howard" };
            UserGroup userGroup;

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);
                groupService = new GroupService(context);

                userService.UpsertUser(user);
                groupService.CreateGroup(group.Title);

                userGroup = new UserGroup() { Group = groupService.FindGroup(1), GroupId = 1, User = userService.Find(1), UserId = 1 };

                user = userService.Find(1);
                user.UserGroups.Add(userGroup);
                userService.UpsertUser(user);

                group = groupService.FindGroup(1);
                group.UserGroups.Add(userGroup);
                groupService.UpdateGroup(group);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);
                groupService = new GroupService(context);

                user = userService.Find(1);
                user.First = "Toast";
                user.Last = "Maker";

                userService.UpsertUser(user);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual<string>("Toast", user.First);
            }
        }
    }
}
