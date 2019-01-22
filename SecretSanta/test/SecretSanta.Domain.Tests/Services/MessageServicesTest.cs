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
    public class MessageServicesTest
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        
        public Message CreateMessage()
        {
            User santa = new User
            {
                FirstName = "Alan",
                LastName = "Watts",
            };

            User recipient = new User
            {
                FirstName = "Calvin",
                LastName = "Hobbes",
            };

            Pairing pairing = new Pairing
            {
                Santa = santa,
                SantaId = santa.Id,
                Recipient = recipient,
                RecipientId = recipient.Id,
                Messages = new List<Message>()
            };

            Message message = new Message
            {
                Content = "Hello world!",
                Pairing = pairing
            };

            return message;
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
        public void AddMessage()
        {
            MessageService messageService;
            Message message = CreateMessage();

            using (var context = new ApplicationDbContext(Options))
            {
                messageService = new MessageService(context);
                messageService.UpsertMessage(message);
                Assert.AreNotEqual(0, message.Id);
            }
        }

        [TestMethod]
        public void StoreMessages()
        {
            MessageService messageService;
            Message message = CreateMessage();

            using (var context = new ApplicationDbContext(Options))
            {
                messageService = new MessageService(context);
                messageService.StoreMessage(message.Pairing, message);
            }
        }
    }
}
