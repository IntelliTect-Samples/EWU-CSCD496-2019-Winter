using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace SecretSanta.Api.Tests
{
#pragma warning disable CS3009 // Base type is not CLS-compliant
    public class CustomWebApplicationFactory<TStartup>
#pragma warning restore CS3009 // Base type is not CLS-compliant
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public CustomWebApplicationFactory()
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }

#pragma warning disable CS3001 // Argument type is not CLS-compliant
        protected override void ConfigureWebHost(IWebHostBuilder builder)
#pragma warning restore CS3001 // Argument type is not CLS-compliant
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlite()
                    .BuildServiceProvider();

                var connection = new SqliteConnection("Data Source=:memory:");
                connection.Open();

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(connection);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                    db.Database.EnsureCreated();
                }
            });
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
#pragma warning disable CS3001 // Argument type is not CLS-compliant
        protected override TestServer CreateServer(IWebHostBuilder builder)
#pragma warning restore CS3001 // Argument type is not CLS-compliant
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return base.CreateServer(builder);
        }

#pragma warning disable CS3002 // Return type is not CLS-compliant
        protected override IWebHostBuilder CreateWebHostBuilder()
#pragma warning restore CS3002 // Return type is not CLS-compliant
        {
            return base.CreateWebHostBuilder();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override IEnumerable<Assembly> GetTestAssemblies()
        {
            return base.GetTestAssemblies();
        }
    }
}
