using System;
using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecretSanta.Api.Models;
using SecretSanta.Domain.Models;
using Serilog;
using Serilog.Events;

namespace SecretSanta.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        context.Database.Migrate();
                    }
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        // Im going to leave these two links here for future reference
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile(
                        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                        optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureLogging((hostingContext, config) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("App Name", "SecretSanta.Api")
                        .WriteTo.SQLite(hostingContext.Configuration.GetConnectionString("DefaultConnection"))
                        .CreateLogger();
                })
                .UseSerilog();
    }
}
