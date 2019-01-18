using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using src.Model;
using src.Models;
using src.Services;
using System;
using System.Collections.Generic;

namespace test.TestServices
{
    [TestClass]
    public class TestMessageService
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }
        private Message Message { get; set; }

        public Message CreateMessage()
        {
            User santa = new User
            {
                FirstName = "Ash",
                LastName = "Ketchum",
            };

            User recep = new User
            {
                FirstName = "Gary",
                LastName = "Oak",
            };

            Pairing pairing = new Pairing
            {
                Santa = santa,
                SantaId = santa.Id,
                Recepiant = recep,
                RecepiantId = recep.Id,
                Messages = new List<Message>()
            };

            Message message = new Message
            {
                MessagePost = "Heated Rivaly!",
                Pairing = pairing,
                PairingId = pairing.Id
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
            Message = CreateMessage();
        }

        [TestCleanup]
        public void CloseConnection()
        {
            SqliteConnection.Close();
        }

        [TestMethod]
        public void AddMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                messageService.Add(Message);
                Assert.AreNotEqual(0, Message.Id);
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
                string msg = message.Pairing.Messages[0].MessagePost;
                Assert.AreEqual(msg, "Heated Rivaly!");
            }
        }

        [TestMethod]
        public void AddPairing()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                Message mesg = CreateMessage();
                messageService.Add(mesg);
                Assert.AreEqual(1, mesg.Id);
            }
        }

        [TestMethod]
        public void UpdateMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                messageService.Add(Message);
                Assert.IsNotNull(messageService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                Message = messageService.Find(1);
                Message.MessagePost = "Old Friends";
                messageService.Update(Message);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                Message tempMesg = messageService.Find(1);
                Assert.AreEqual("Old Friends", tempMesg.MessagePost);
            }
        }

        [TestMethod]
        public void FindMessage()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                Assert.IsNull(messageService.Find(1));
                messageService.Add(Message);
                Assert.IsNotNull(messageService.Find(1));
            }

            using (var context = new ApplicationDbContext(Options))
            {
                MessageService messageService = new MessageService(context);
                Message foundMessage = messageService.Find(1);
                Assert.IsNotNull(foundMessage);
            }
        }

        [TestMethod]
        public void IsMessageNull_Correct()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService MessageServ = new MessageService(context);
                bool isMessageNull = MessageServ.IsMessageNull(Message);
                Assert.IsFalse(isMessageNull);
            }
        }

        [TestMethod]
        public void IsMessageNull_Incorrect()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                MessageService MessageServ = new MessageService(context);
                Message = null;
                bool isMessageNull = MessageServ.IsMessageNull(Message);
                Assert.IsTrue(isMessageNull);
            }
        }
    }
}