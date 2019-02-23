using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecretSanta.Api.Controllers;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
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

        [TestInitialize]
        public void CreateWebFactory()
        {
            Factory = new CustomWebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task AddUserViaApi_FailsDueToMissingFirstName()
        {
            HttpClient client = Factory.CreateClient();

            UserInputViewModel viewModel = new UserInputViewModel
            {
                FirstName = "",
                LastName = "Montoya"
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", viewModel);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            ProblemDetails problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(result);

            JObject errors = problemDetails.Extensions["errors"] as JObject;

            JProperty firstError = (JProperty)errors.First;

            JToken errorMessage = firstError.Value[0];

            Assert.AreEqual("The FirstName field is required.", ((JValue)errorMessage).Value);
        }

        [TestMethod]
        public async Task AddUserViaApi_CompletesSuccessfully()
        {
            HttpClient client = Factory.CreateClient();

            UserInputViewModel userViewModel = new UserInputViewModel
            {
                FirstName = "Inigo",
                LastName = "Montoya"
            };

            HttpResponseMessage response = await client.PostAsJsonAsync("/api/users", userViewModel);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            string result = await response.Content.ReadAsStringAsync();

            UserViewModel resultViewModel = JsonConvert.DeserializeObject<UserViewModel>(result);

            Assert.AreEqual(userViewModel.FirstName, resultViewModel.FirstName);
        }




    }
}
