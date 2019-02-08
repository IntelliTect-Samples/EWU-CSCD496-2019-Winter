using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SecretSanta.Api.Models;
using System.Net;
using System.Net.Http;



namespace SecretSanta.Api.Tests.Controllers
{
    public class ControllerTestBase
    {
        protected CustomWebApplicationFactory<Startup> Factory { get; set; }

        static ControllerTestBase()
        {
            ConfigureAutoMapper();
        }

        protected ControllerTestBase()
        {
            CreateWebFactory();
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AutoMapperProfileConfig()));
        }

        public static void CreateWebFactory()
        {
            
        }

       
    }

}
