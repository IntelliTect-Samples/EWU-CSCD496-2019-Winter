using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecretSanta.Web.ApiModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        public GroupsController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Groups = await secretSantaClient.GetGroupsAsync().ConfigureAwait(false);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
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
                    fetchedGroup = await secretSantaClient.GetGroupAsync(id).ConfigureAwait(false);
                }
                catch (SwaggerException se)
                {
                    ModelState.TryAddModelError("", se.Message);
                }
            }
            return View(fetchedGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        var group = await secretSantaClient.CreateGroupAsync(viewModel).ConfigureAwait(false);

                        if (group != null)
                        {
                            result = RedirectToAction(nameof(Index));
                        }
                    }
                    catch (SwaggerException se)
                    {
                        ModelState.TryAddModelError("", se.Message);
                    }
                }
            }
            return result;
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

                        await secretSantaClient.UpdateGroupAsync(viewModel.Id, new GroupInputViewModel
                        {
                            Name = viewModel.Name
                        }).ConfigureAwait(false);

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
                    await secretSantaClient.DeleteGroupAsync(id).ConfigureAwait(false);

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
