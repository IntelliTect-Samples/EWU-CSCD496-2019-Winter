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
                UsersService.Add(User);
                Assert.AreEqual(1, User.Id);
            }
        }

        [TestMethod]
        public void UpdateGiftAfterAdd()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                UsersService.Add(User);
                Assert.IsNotNull(UsersService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User tempUser = UsersService.Find(1);
                tempUser.FirstName = "Gary";
                tempUser.LastName = "Oak";
                UsersService.Update(tempUser);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User = UsersService.Find(1);
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
                Assert.IsNull(UsersService.Find(1));
                UsersService.Add(User);
                Assert.IsNotNull(UsersService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User foundUser = UsersService.Find(1);
                Assert.IsNotNull(foundUser);
            }
        }

        [TestMethod]
        public void NotFindUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User = UsersService.Find(1);
                Assert.IsNull(User);
            }
        }

        [TestMethod]
        public void IsFirstNameNotNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                bool isNull = UsersService.IsFirstNameNotNull(User);
                Assert.IsTrue(isNull);
            }
        }

        [TestMethod]
        public void IsFirstNameNotNull_InCorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User.FirstName = null;
                bool isNull = UsersService.IsFirstNameNotNull(User);
                Assert.IsFalse(isNull);
            }
        }

        [TestMethod]
        public void IsLastNameNotNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                bool isNull = UsersService.IsLastNameNotNull(User);
                Assert.IsTrue(isNull);
            }
        }

        [TestMethod]
        public void IsLastNameNotNull_InCorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User.LastName = null;
                bool isNull = UsersService.IsLastNameNotNull(User);
                Assert.IsFalse(isNull);
            }
        }

        [TestMethod]
        public void IsFullNameNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                bool isNotNull = UsersService.IsFullNameNull(User);
                Assert.IsFalse(isNotNull);
            }
        }

        [TestMethod]
        public void IsFullNameNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User.FirstName = null;
                User.LastName = null;
                bool isNotNull = UsersService.IsFullNameNull(User);
                Assert.IsTrue(isNotNull);
            }
        }

        [TestMethod]
        public void IsGiftListNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                bool isNull = UsersService.IsGiftListNull(User);
                Assert.IsFalse(isNull);
            }
        }

        [TestMethod]
        public void IsGiftListNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User.GiftList = null;
                bool isNull = UsersService.IsGiftListNull(User);
                Assert.IsTrue(isNull);
            }
        }

        [TestMethod]
        public void IsUserNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                bool isNull = UsersService.IsUserNull(User);
                Assert.IsFalse(isNull);
            }
        }

        [TestMethod]
        public void IsUserNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UsersService = new UsersService(context);
                User testUser = null;
                bool isNull = UsersService.IsUserNull(testUser);
                Assert.IsTrue(isNull);
            }
        }
    }
}
