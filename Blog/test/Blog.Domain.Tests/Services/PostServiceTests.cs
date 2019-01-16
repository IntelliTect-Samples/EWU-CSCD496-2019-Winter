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
using System.Linq;

namespace Blog.Domain.Tests.Services
{
    [TestClass]
    public class PostServiceTests
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

        Post CreateNewPost()
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
                User = user
            };

            var tag = new Tag
            {
                Name = "C#"
            };

            post.PostTags = new List<PostTag>();
            post.PostTags.Add(new PostTag
            {
                Tag = tag
            });

            return post;
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
        public void AddPost()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var myPost = CreateNewPost();

                var persistedPost = service.AddPost(myPost);

                Assert.AreNotEqual(0, persistedPost.Id);
            }
        }

        [TestMethod]
        public void FindPost()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var myPost = CreateNewPost();

                service.AddPost(myPost);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var fetchedPost = service.Find(1);

                Assert.AreEqual("My First Post", fetchedPost.Title);
                Assert.AreEqual("Inigo", fetchedPost.User.FirstName);
                Assert.AreEqual("C#", fetchedPost.PostTags[0].Tag.Name);
            }
        }

        [TestMethod]
        public void UpdateUserOnPost()
        {
            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var myPost = CreateNewPost();

                service.AddPost(myPost);

                UserService userService = new UserService(context);
                var myUser = new User { FirstName = "Princess", LastName = "Buttercup" };
                userService.AddUser(myUser);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var fetchedPost = service.Find(1);

                Assert.AreEqual("My First Post", fetchedPost.Title);
                Assert.AreEqual("Inigo", fetchedPost.User.FirstName);

                fetchedPost.UserId = 2;

                service.UpdatePost(fetchedPost);
            }

            using (var context = new ApplicationDbContext(Options))
            {
                PostService service = new PostService(context);
                var fetchedPost = service.Find(1);

                Assert.AreEqual("My First Post", fetchedPost.Title);
                Assert.AreEqual("Princess", fetchedPost.User.FirstName);
            }
        }
    }
}
