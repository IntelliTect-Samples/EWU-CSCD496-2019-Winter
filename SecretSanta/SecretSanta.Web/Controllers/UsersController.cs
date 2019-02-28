using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ApiModels;

namespace SecretSanta.Web.Controllers
{

    public class UsersController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }

        public UsersController(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        /* DISPLAY ALL USERS */

        public async Task<IActionResult> Index()
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    ViewBag.Users = await secretSantaClient.GetAllUsersAsync();

                    /*var content = await response.Content.ReadAsStringAsync();

                    ViewBag.Users = JsonConvert.DeserializeObject<ICollection<UserViewModel>>(content);
                    //parsing to put into catch all, put from controller to view*/
                }
            }
            return View();
        }

        /* ADD A USER */

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
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);

                    if (await secretSantaClient.CreateUserAsync(viewModel) == null) //does not create valid user
                    {
                        ModelState.AddModelError("", "Cannot add invalid user. First name is required!");
                    }
                    else
                    {
                        result = RedirectToAction(nameof(Index));
                    }
                }
            }

            return result;
        }

        /* EDIT A USER */

        [HttpGet]
        public async Task<IActionResult> Edit(int userId)
        {
            if (userId == 0)
            {
                ModelState.AddModelError("", "The id is indeed" + userId);
                return View(null);
            }
            UserViewModel result = null;

            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    result = await secretSantaClient.GetUserAsync(userId);
                }
                catch (SwaggerException e)
                {
                    ModelState.AddModelError("", "There shouldn't be an error" + userId);
                }
            }
            return View(result);

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
                        SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);

                        await secretSantaClient.UpdateUserAsync(viewModel.Id, new UserInputViewModel
                        {
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName
                        });

                        result = RedirectToAction(nameof(Index));
                        
                    }
                    catch (SwaggerException e)
                    {
                        ModelState.AddModelError("", "Cannot update existing using an invalid user");
                    }
                }
            }

            return result;
        }

        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();

            using (HttpClient httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    SecretSantaClient secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
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