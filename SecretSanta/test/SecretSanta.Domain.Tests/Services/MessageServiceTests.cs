using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

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

        private static Message CreateMessage()
        {
            /*
             * public User Recipient { get; set; }
             * public User Sender { get; set; }
             * public string Text { get; set; }
             */
            var recipient = new User()
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };
            
            var sender = new User()
            {
                FirstName = "Princess",
                LastName = "Buttercup"
            };

            var message = new Message()
            {
                Recipient = recipient,
                Sender = sender,
                Text = "What kind of Roomba do you want?"
            };

            return message;
        }

        [TestMethod]
        public void UpsertMessage_TestWithStaticMessage_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var myMessage = CreateMessage();

                var addedMessage = service.UpsertMessage(myMessage);
                
                Assert.AreEqual(1, addedMessage.Id);
            }
        }
        
        [TestMethod]
        public void UpsertMessage_TestUpdateMessage_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var myMessage = CreateMessage();

                // Add message
                service.UpsertMessage(myMessage);
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var retrievedMessage = service.Find(1);

                // Update message
                retrievedMessage.Text = "Updated Text";
                service.UpsertMessage(retrievedMessage);
            }
            
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var retrievedMessage = service.Find(1);

                // Validate change
                Assert.AreEqual("Updated Text", retrievedMessage.Text);
            }
        }

        [TestMethod]
        public void DeleteMessage_TestRemoveStaticMessage_Success()
        {
            var myMessage = CreateMessage();
            
            // Add message into DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);

                var addedMessage = service.UpsertMessage(myMessage);
                
                Assert.AreEqual(1, addedMessage.Id);
            }
            
            // Remove message from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);

                service.DeleteMessage(myMessage);

                Assert.IsNull(service.Find(1));
            }
        }

        [TestMethod]
        public void FindMessage_TestFindStaticMessage_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var myMessage = CreateMessage();

                var persistedUser = service.UpsertMessage(myMessage);
                
                Assert.AreEqual(1, persistedUser.Id);
            }
            
            // Remove message from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new MessageService(context);
                var myMessage = CreateMessage();

                var foundMessage = service.Find(1);
                
                Assert.AreEqual(1, foundMessage.Id);
                Assert.AreNotEqual(0, foundMessage.Sender.Id);
                Assert.AreNotEqual(0, foundMessage.Recipient.Id);
            }
        }
    }
}