using Blog.Domain.Models;
using Blog.Domain.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        private SqliteConnection SqliteConnection { get; set; }
        private DbContextOptions<ApplicationDbContext> Options { get; set; }

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
        public void AddPost_ReturnsSuccess()
        {
            PostService postService;

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                Author author = new Author { Name = "Michael Stokesbary" };
                Post post = new Post
                {
                    Body = "This is my first post and it is awesome",
                    Author = author,
                    Title = "First Post",
                    AuthoredOn = DateTime.Now,
                    Slug = "first-post"
                };

                Assert.IsTrue(postService.AddPost(post));

                Assert.AreEqual(1, post.Id);
                Assert.AreEqual("Michael Stokesbary", post.Author.Name);

                post.Body = "Just some new body text";

                Assert.IsTrue(postService.AddPost(post));
            }
        }
    }
}
