using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace test.TestServices
{
    [TestClass]
    public class TestUserService
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        private UsersService UsersService { get; set; }
        private User User { get; set; }

        private static User CreateUser()
        {
            var user = new User
            {
                FirstName = "Ash",
                LastName = "Ketchum"
            };
            user.GiftList = new List<Gift> {};
            return user;
        }


        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .EnableSensitiveDataLogging()
                .Options;

            using (var context = new ApplicationDbContext(Options))
            {
                context.Database.EnsureCreated();
            }
            User = CreateUser();
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }


        [TestMethod]
        public void AddUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                UsersService.AddUser(User);
                Assert.AreEqual(1, User.Id);
            }
        }

        [TestMethod]
        public void UpdateGiftAfterAdd()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                UsersService.AddUser(User);
                Assert.IsNotNull(UsersService.FindUser(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User tempUser = UsersService.FindUser(1);
                tempUser.FirstName = "Gary";
                tempUser.LastName = "Oak";
                UsersService.UpdateUser(tempUser);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User = UsersService.FindUser(1);
                Assert.AreEqual("Gary", User.FirstName);
                Assert.AreEqual("Oak", User.LastName);
            }
        }

        [TestMethod]
        public void FindUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                Assert.IsNull(UsersService.FindUser(1));
                UsersService.AddUser(User);
                Assert.IsNotNull(UsersService.FindUser(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User foundUser = UsersService.FindUser(1);
                Assert.IsNotNull(foundUser);
            }
        }

        [TestMethod]
        public void NotFindUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User = UsersService.FindUser(1);
                Assert.IsNull(User);
            }
        }
    }
}
