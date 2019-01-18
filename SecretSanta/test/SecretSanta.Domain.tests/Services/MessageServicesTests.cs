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
    class MessageServicesTests
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

        public Message CreateMessage()
        {
            var message = new Message
            {
                MessageContent = "content",
                Recipiant = CreateUser(),
                Santa = CreateUser()
                
            };

            return message;
        }

        [TestMethod]
        public void AddNewMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageServices messageServices = new MessageServices(context);
                Message m = CreateMessage();

                Message persistedMessage = messageServices.AddMessage(m);

                Assert.AreEqual(1, persistedMessage.Id);

            }
        }

        
    }
}
