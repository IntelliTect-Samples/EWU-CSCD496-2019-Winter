using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class ApplicationDbContextTests
    {
        private SqliteConnection SqliteConnection {get; set;}
        private DbContextOptions<ApplicationDbContext> Options {get; set;}

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
        public void CreateUser()
        {
            User user;

            using (var context = new ApplicationDbContext(Options))
            {
                user = new User {FirstName = "Inigo", LastName = "Montoya"};
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(Options))
            {
                user = context.Users.SingleOrDefault(u => u.FirstName == "Inigo");

                Assert.AreEqual("Inigo", user.FirstName);
            }
        }
    }
}