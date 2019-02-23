
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecretSanta.Api.ViewModels;
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
            var client = Factory.CreateClient();

            var viewModel = new UserInputViewModel
            {
                FirstName = "",
                LastName = "Montoya"
            };

            var response = await client.PostAsJsonAsync("/api/users", viewModel);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var result = await response.Content.ReadAsStringAsync();

            var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(result);

            var errors = problemDetails.Extensions["errors"] as JObject;

            var firstError = (JProperty)errors.First;

            var errorMessage = firstError.Value[0];

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
