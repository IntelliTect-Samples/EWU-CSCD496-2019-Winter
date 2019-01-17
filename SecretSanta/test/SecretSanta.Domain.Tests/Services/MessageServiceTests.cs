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
    public class MessageServiceTests
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

        private Message CreateMessage()
        {
            User toUser = new User();
            User fromUser = new User();

            return new Message { ToUser = toUser, FromUser = fromUser, Content = "Message Content" };
        }

        [TestMethod]
        public void AddMessage()
        {
            using (var dbContext = new ApplicationDbContext(Options))
            {
                MessageService service = new MessageService(dbContext);
                Message message = CreateMessage();

                service.AddMessage(message);
            }

            using (var dbContext = new ApplicationDbContext(Options))
            {
                MessageService service = new MessageService(dbContext);
                var fetchedMessage = service.Find(1);

                Assert.AreEqual("Message Content", fetchedMessage.Content);
            }
        }
    }
}
