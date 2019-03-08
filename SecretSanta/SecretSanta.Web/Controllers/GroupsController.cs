using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ApiModels;

namespace SecretSanta.Web.Controllers
{
    public class GroupsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public GroupsController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        /* DISPLAY ALL USERS */

        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Groups = await secretSantaClient.GetGroupsAsync();
            }
            return View();
        }

        /* ADD A Group */

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupInputViewModel viewModel)
        {
            IActionResult result = View();
            if (viewModel.Name == null || viewModel.Name.Trim().Length == 0)
            {
                ModelState.AddModelError("", "Cannot add a empty name to the group.");
            }

            else if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);

                    if (await secretSantaClient.CreateGroupAsync(viewModel) == null) //does not create valid user
                    {
                        ModelState.AddModelError("", "Cannot add invalid Group. Name is required!");
                    }
                    else
                    {
                        result = RedirectToAction(nameof(Index));
                    }
                }
            }

            return result;
        }

        /* EDIT A Group */

        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GroupViewModel result = null;

            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    result = await secretSantaClient.GetGroupAsync(id);
                }
                catch (SwaggerException e)
                {
                    ModelState.AddModelError("", $"{e}: The Group with ID {id} does not exist and cannot be edited");
                }
            }
            return View(result);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupViewModel viewModel)
        {
            IActionResult result = View();

            if (viewModel.Name == null || viewModel.Name.Trim().Length == 0)
            {
                ModelState.AddModelError("", "Cannot edit a empty name to the group.");
            }

            else if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);

                        await secretSantaClient.UpdateGroupAsync(viewModel.Id, new GroupInputViewModel
                        {
                            Name = viewModel.Name,
                        });

                        result = RedirectToAction(nameof(Index));

                    }
                    catch (SwaggerException e)
                    {
                        ModelState.AddModelError("", $"{e}: Cannot update existing using an invalid group");
                    }
                }
            }

            return result;
        }

        /* Delete A Group */

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();

            using (HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    await secretSantaClient.DeleteGroupAsync(id);

                    result = RedirectToAction(nameof(Index));
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", $"{se}: The ID {id} does not coorespond to a group, cannot be deleted.");
                }
            }
            return result;
        }
    }
}