using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class PairingServiceTests
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

        private static User CreateUser(string fName = "Inigo", string lName = "Montoya")
        {
            var user = new User
            {
                FirstName = fName,
                LastName = lName
            };

            return user;
        }

        /*
         * public string Title { get; set; }
         * [NotMapped] public List<User> Users { get; set; } // Ignoring the many-many connection for now
         */

        private static Pairing CreatePairing(string groupTitle = "The Title")
        {
            var santa = CreateUser();

            var recipient = CreateUser("Princess", "Buttercup");

            var users = new List<User> {CreateUser("Count")};

            var group = new Group
            {
                Title = "The Title",
                Users = users
            };

            var pairing = new Pairing
            {
                Santa = santa,
                Recipient = recipient,
                Group = group
            };

            return pairing;
        }

        private static List<Pairing> CreatePairings(int numberToCreate = 2)
        {
            var pairings = new List<Pairing>();

            for (var i = 0; i < numberToCreate; i++) pairings.Add(CreatePairing($"GroupTitle{i}"));

            return pairings;
        }


        [TestMethod]
        public void UpsertPairing_TestWithStaticPairing_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var myPairing = CreatePairing();

                var addedPairing = service.UpsertPairing(myPairing);

                Assert.AreEqual(1, addedPairing.Id);
            }
        }

        [TestMethod]
        public void UpsertPairing_TestUpdatePairing_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var myPairing = CreatePairing();

                // Add pairing
                service.UpsertPairing(myPairing);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var retrievedPairing = service.Find(1);

                // Update pairing
                retrievedPairing.Santa = CreateUser("Count", "Rugen");
                service.UpsertPairing(retrievedPairing);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var retrievedPairing = service.Find(1);

                // Validate change
                Assert.AreEqual("Count", retrievedPairing.Santa.FirstName);
            }
        }

        [TestMethod]
        public void DeletePairing_TestRemoveStaticPairing_Success()
        {
            var myPairing = CreatePairing();

            // Add pairing into DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);

                var addedPairing = service.UpsertPairing(myPairing);

                Assert.AreEqual(1, addedPairing.Id);
            }

            // Remove pairing from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);

                service.DeletePairing(myPairing);

                Assert.IsNull(service.Find(1));
            }
        }

        [TestMethod]
        public void FindPairing_TestFindStaticPairing_Success()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var myPairing = CreatePairing();

                var addedPairing = service.UpsertPairing(myPairing);

                Assert.AreEqual(1, addedPairing.Id);
            }

            // Remove pairing from DB
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var myPairing = CreatePairing();

                var foundPairing = service.Find(1);

                Assert.AreEqual(1, foundPairing.Id);
                Assert.AreNotEqual(0, foundPairing.Santa.Id);
                Assert.AreNotEqual(0, foundPairing.Recipient.Id);
            }
        }

        [TestMethod]
        public void FetchPairings_TestWithStaticPairings_Success()
        {
            // arrange
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var messages = CreatePairings();

                foreach (var cur in messages) service.UpsertPairing(cur);
            }

            // act
            using (var context = new ApplicationDbContext(Options))
            {
                var service = new PairingService(context);
                var fetchedPairings = service.FetchAll();

                // assert
                for (var i = 0; i < fetchedPairings.Count; i++)
                    Assert.AreEqual(i + 1, fetchedPairings[i].Id);
            }
        }
    }
}