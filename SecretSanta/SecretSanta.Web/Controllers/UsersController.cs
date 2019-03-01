using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecretSanta.Web.ApiModels;
using AutoMapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get;  }

        public UsersController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Users = await secretSantaClient.GetAllUsersAsync();
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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UserViewModel fetchedUser = null;
            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        fetchedUser = await secretSantaClient.GetUserAsync(id);
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }
            return View(fetchedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.UpdateUserAsync(viewModel.Id, Mapper.Map<UserInputViewModel>(viewModel));

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.AddModelError("", se.Message);
                    }
                }
            }

            return result;
        }

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    await secretSantaClient.DeleteUserAsync(id);

                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }

            return result;
        }
    }
}

