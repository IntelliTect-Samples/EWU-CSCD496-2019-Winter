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
    public class GroupServiceTests
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
        public void AddUser()
        {
            GroupService groupService;

            User user = new User() { First = "Brad", Last = "Howard", Groups = new List<Group>(), Gifts = new List<Gift>() };

            using (var context = new SecretSantaDbContext(Options))
            {
                groupService = new GroupService(context);

                groupService.CreateGroup("test");

                groupService.AddUser(user, 1);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                groupService = new GroupService(context);

                Assert.AreEqual(true, groupService.HasUser(user, 1));
            }
        }

        [TestMethod]
        public void RemoveUser()
        {
            GroupService groupService;

            User user = new User() { First = "Brad", Last = "Howard", Groups = new List<Group>(), Gifts = new List<Gift>() };

            using (var context = new SecretSantaDbContext(Options))
            {
                groupService = new GroupService(context);

                groupService.CreateGroup("test");

                groupService.AddUser(user, 1);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                groupService = new GroupService(context);

                groupService.RemoveUser(user, 1);

                Assert.AreEqual(false, groupService.HasUser(user, 1));
            }
        }
    }
}
