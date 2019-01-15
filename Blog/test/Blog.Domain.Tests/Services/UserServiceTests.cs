using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Models;
using Blog.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

        ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            });

            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        private User CreateInitialData()
        {
            User user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            return user;
        }

        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(SqliteConnection)
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging()
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
            UserService userService;

            var user = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                userService.UpsertUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual("Inigo", user.FirstName);
            }
        }

        [TestMethod]
        public void UpdateUser()
        {
            UserService userService;

            var user = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                userService.UpsertUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);
                user.FirstName = "Princess";
                user.LastName = "Buttercup";

                userService.UpsertUser(user);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);

                user = userService.Find(1);

                Assert.AreEqual("Princess", user.FirstName);
            }
        }
    }
}
