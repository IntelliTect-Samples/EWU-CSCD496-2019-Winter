﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ApiModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }

        public GroupsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Groups = await secretSantaClient.GetGroupsAsync();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(GroupInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    var response = await httpClient.PostAsJsonAsync<GroupInputViewModel>("/api/groups", viewModel);
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
            GroupViewModel fetchedGroup = null;

            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    fetchedGroup = await secretSantaClient.GetGroupAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }
            return View(fetchedGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.UpdateGroupAsync(viewModel.Id, Mapper.Map<GroupInputViewModel>(viewModel));

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
                    await secretSantaClient.DeleteGroupAsync(id);

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
