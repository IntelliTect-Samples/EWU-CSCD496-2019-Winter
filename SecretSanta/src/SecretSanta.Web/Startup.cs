﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace SecretSanta.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddHttpClient("SecretSantaApi", config =>
            {
                config.BaseAddress = new Uri(Configuration.GetSection("SecretSantaApi").GetValue<string>("BaseUri"));
            });

            var dependencyContext = DependencyContext.Default;
            var assemblies = dependencyContext.RuntimeLibraries.SelectMany(lib =>
                lib.GetDefaultAssemblyNames(dependencyContext)
                    .Where(a => a.Name.Contains("SecretSanta", StringComparison.Ordinal)).Select(Assembly.Load)).ToArray();
            services.AddAutoMapper(assemblies);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
