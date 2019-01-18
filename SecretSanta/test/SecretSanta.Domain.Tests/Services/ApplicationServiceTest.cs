using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Tests.Services
{
    [TestClass]
    public class ApplicationServiceTest
    {
        private bool DEBUG = false;     //set true to enable logging to console.

        protected SqliteConnection SqliteConnection { get; set; }
        protected DbContextOptions<ApplicationDbContext> Options { get; set; }

        ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name,
                               LogLevel.Information);
            });
            return serviceCollection.BuildServiceProvider()
                   .GetService<ILoggerFactory>();
        }


        [TestInitialize]
        public void OpenConnection()
        {
            SqliteConnection = new SqliteConnection("DataSource=:memory:");
            SqliteConnection.Open();

            if (DEBUG)
            {
                Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(SqliteConnection)
                    .UseLoggerFactory(GetLoggerFactory())
                    .EnableSensitiveDataLogging()
                    .Options;
            }
            else
            {
                Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(SqliteConnection)
                    .Options;
            }
            

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
    }
}
