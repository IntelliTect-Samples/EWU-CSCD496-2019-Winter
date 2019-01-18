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

    /*[TestClass]
    public class UserServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(SqliteConnection).Options;

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

        //helper methods
        public User CreateUser()
        {
                var user = new User
                {
                    FirstName = "fname",
                    LastName = "lname"
                };

            return user;
        }

        [TestMethod]
        public void AddSingleNewUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserServices userServices = new UserServices(context);
                User u = CreateUser();

                User persistedUser = userServices.AddUpdateUser(u);
                Assert.AreEqual(1, persistedUser.Id);
            }
            
        }

        [TestMethod]
        public void AddMultipleNewUsers()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserServices userServices = new UserServices(context);
                User u1 = CreateUser();
                User u2 = CreateUser();
                User u3 = CreateUser();

                User persistedUser1 = userServices.AddUpdateUser(u1);
                User persistedUser2 = userServices.AddUpdateUser(u2);
                User persistedUser3 = userServices.AddUpdateUser(u3);

                Assert.AreEqual(1, persistedUser1.Id);
                Assert.AreEqual(2, persistedUser2.Id);
                Assert.AreEqual(3, persistedUser3.Id);
            }
        }

        [TestMethod]
        public void AddNewUserThenUpdateSameUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserServices userServices = new UserServices(context);
                User u1 = CreateUser();

                User persistedUser1 = userServices.AddUpdateUser(u1);

                u1.FirstName = "New";

                User persistedUser2 = userServices.AddUpdateUser(u1);

                Assert.AreEqual(persistedUser1.Id, persistedUser2.Id);

            }
        }

    
    }*/
}
