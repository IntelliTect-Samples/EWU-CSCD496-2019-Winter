﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ApiModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
                var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
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
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.CreateUserAsync(viewModel);

                        result = RedirectToAction(nameof(Index));
                    }
                    catch(SwaggerException se)
                    {
                        ViewBag.ErrorMessage = se.Message;
                    }
                }
            }

            return result;
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UserViewModel fetchedUser = null;

            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedUser = await secretSantaClient.GetUserAsync(id);
                }
                catch (SwaggerException swaggerException)
                {
                    ModelState.AddModelError("", swaggerException.Message);
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
                        await secretSantaClient.UpdateUserAsync(viewModel.Id, new UserInputViewModel
                        {
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName
                        });

                        result = RedirectToAction(nameof(Index));
                    }
                    catch (SwaggerException se)
                    {
                        ViewBag.ErrorMessage = se.Message;
                    }
                }
            }

            return result;
        }
    }
}
