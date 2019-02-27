using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{

    public class UsersController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public UsersController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                var response = await httpClient.GetAsync("/api/users");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    ViewBag.Users = JsonConvert.DeserializeObject<ICollection<UserViewModel>>(content);
                    //parsing to put into catch all, put from controller to view
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    var response = await httpClient.PostAsJsonAsync<UserInputViewModel>("/api/users", viewModel);
                    ViewBag.StatusCode = response.StatusCode;

                    if (response.IsSuccessStatusCode)
                    {
                        result = RedirectToAction(nameof(Index));
                    }
                }
            }

            return result;
        }
    }
}