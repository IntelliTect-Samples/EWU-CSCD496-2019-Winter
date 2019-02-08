using AutoMapper;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.Models;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private CustomWebApplicationFactory<Startup> Factory { get; set; }

        public UserControllerTests()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task AddUserThroughApi_FailMissingFirstName()
        {
            HttpClient client = Factory.CreateClient();

            UserInputViewModel viewModel = new UserInputViewModel
            {
                FirstName = "",
                LastName = "Hau"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("/api/User", content);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }

        [TestMethod]
        public async Task AddUserThroughApi_SuccessAdded()
        {
            HttpClient client = Factory.CreateClient();

            UserInputViewModel viewModel = new UserInputViewModel
            {
                FirstName = "Fox",
                LastName = "Hau"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync("/api/User", content);

            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
        }
    }
}
