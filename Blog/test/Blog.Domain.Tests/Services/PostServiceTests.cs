using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Models;
using Blog.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Tests.Services
{
    [TestClass]
    public class PostServiceTests
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

        [TestMethod]
        public void CreatePost()
        {
            PostService postService;

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
    }
}
