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
    public class MessageServiceTests
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
        public void StoreMassage()
        {
            MessageService messageService;

            User from = new User() { First = "Brad", Last = "Howard" };
            User to = new User() { First = "Rena", Last = "Hau" };
            string body = "This is a test body";

            Message message = new Message() { UserFrom = from, UserTo = to, MessageBody = body };

            using (var context = new SecretSantaDbContext(Options))
            {
                messageService = new MessageService(context);

                messageService.StoreMassage(message);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                messageService = new MessageService(context);

                Assert.AreEqual("Brad", messageService.FindMessage(1).UserFrom.First);
            }
        }
    }
}
