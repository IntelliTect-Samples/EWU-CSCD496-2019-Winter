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
        ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name,
                               LogLevel.Information);
            });
            return serviceCollection.BuildServiceProvider().
            GetService<ILoggerFactory>();
        }
        User CreateUser()
        {
            var user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            var post = new Post
            {
                Title = "My First Post",
                Content = "Here is some great content",
                IsPublished = false,
                PostedOn = DateTime.Now,
                Slug = "my-first-post",

            };

            user.Posts = new List<Post>();
            user.Posts.Add(post);

            return user;
        }

        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

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
        public void AddUser()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.AddUser(myUser);

                Assert.AreNotEqual(0, persistedUser.Id);
            }
        }

        [TestMethod]
        public void FindUser()
        {
            // arrange
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var myUser = CreateUser();

                var persistedUser = service.AddUser(myUser);
            }

            // act
            using (var context = new ApplicationDbContext(Options))
            {
                UserService service = new UserService(context);
                var fetchedUser = service.Find(1);

                // assert
                Assert.AreEqual(1, fetchedUser.Posts.Count);
            }
        }
    }
}
