using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                        context.Database.EnsureCreated();
                    }
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host Error, ~~END PROCESS~~");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(
                (buildContext, configure) => 
                {
                    var env = buildContext.HostingEnvironment;
                    configure.AddJsonFile("appsetting.json", true, true)
                                .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
                                .AddEnvironmentVariables();

                    if (args != null) configure.AddCommandLine(args);
                }
                )
                .ConfigureLogging(
                (buildContext, logBuilder) =>
                {

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(buildContext.Configuration)
                        .Enrich.WithProperty("App Name", "SecretSanta.Api")
                        .WriteTo.File
                        (
                            path: Path.Combine(Directory.GetCurrentDirectory(), @"LogFiles\log.log"),
                            fileSizeLimitBytes: 1000000,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            flushToDiskInterval: TimeSpan.FromSeconds(1)
                        )
                        .WriteTo.ApplicationInsights("118d94a5-274f-4235-81f4-335c3ec4afcd", TelemetryConverter.Events)
                        .CreateLogger();
                }
                )
                .UseSerilog();
    }
}
