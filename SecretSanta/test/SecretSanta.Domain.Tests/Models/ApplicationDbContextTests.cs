using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Tests
{
    [TestClass]
    public class ApplicationDbContextTests
    {
        private SqliteConnection SqliteConnection {get; set;}
        private DbContextOptions<ApplicationDbContext> Options {get; set;}

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging()
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

        [TestMethod]
        public void CreateUserAndNotAbleToFetch()
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
                user = context.Users.SingleOrDefault(u => u.FirstName == "Michael");

                Assert.IsNull(user);
            }
        }
    }
}