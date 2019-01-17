using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

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
            UserService userService;

            User uFrom = new User() { First = "Brad", Last = "Howard" };
            User uTo = new User() { First = "Rena", Last = "Hau" };
            string body = "This is a test body";

            Message message = new Message() { UserFrom = uFrom, UserTo = uTo, MessageBody = body };

            using (var context = new SecretSantaDbContext(Options))
            {
                messageService = new MessageService(context);
                userService = new UserService(context);

                userService.UpsertUser(uFrom);
                userService.UpsertUser(uTo);

                message.UserToId = uTo.Id;
                message.UserFromId = uFrom.Id;

                messageService.StoreMassage(message);
            }

            using (var context = new SecretSantaDbContext(Options))
            {
                messageService = new MessageService(context);
                userService = new UserService(context);

                Assert.AreEqual(1, messageService.FindMessage(1).UserFromId);
            }
        }
    }
}
