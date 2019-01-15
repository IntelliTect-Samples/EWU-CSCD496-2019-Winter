using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Models;
using Blog.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Domain.Tests.Services
{
    [TestClass]
    public class PostServiceTests
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

        private Post CreateInitialData()
        {
            User user = new User
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            Post post = new Post
            {
                Title = "First Post",
                Body = "Simple body for a blog post",
                CreatedOn = DateTime.Now,
                IsPublished = false,
                Slug = "first-post",
                User = user
            };

            return post;
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
        public void CreatePost()
        {
            PostService postService;

            var post = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                postService.UpsertPost(post);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                post = postService.Find(1);

                Assert.AreEqual("First Post", post.Title);
            }
        }

        [TestMethod]
        public void UpdatePost()
        {
            PostService postService;

            var post = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                postService.UpsertPost(post);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                post = postService.Find(1);

                Assert.IsFalse(post.IsPublished);

                post.IsPublished = true;

                postService.UpsertPost(post);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                post = postService.Find(1);

                Assert.IsTrue(post.IsPublished);
            }
        }

        [TestMethod]
        public void DeletePost()
        {
            PostService postService;

            var post = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                postService.UpsertPost(post);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                postService.DeletePost(1);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);

                post = postService.Find(1);

                Assert.IsNull(post);
            }
        }

        [TestMethod]
        public void ChangeUser()
        {
            PostService postService;
            UserService userService;

            var post = CreateInitialData();

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);
                userService = new UserService(context);

                postService.UpsertPost(post);

                userService.UpsertUser(new User { FirstName = "Princess", LastName = "Buttercup" });
            }

            using (var context = new ApplicationDbContext(Options))
            {
                postService = new PostService(context);
                userService = new UserService(context);

                // var user = userService.Find(2);

                post = postService.Find(1);

                Assert.AreEqual("Inigo", post.User.FirstName);

                post.UserId = 2;

                postService.UpsertPost(post);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                userService = new UserService(context);
                postService = new PostService(context);

                var users = userService.FetchAll();
                post = postService.Find(1);

                Assert.AreEqual(2, users.Count);

                Assert.AreEqual("Princess", post.User.FirstName);
            }
        }
    }
}
