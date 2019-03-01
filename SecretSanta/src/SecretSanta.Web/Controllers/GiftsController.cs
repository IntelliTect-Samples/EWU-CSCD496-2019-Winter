using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Web.Controllers
{
    public class GiftsController : Controller
    {
        private IHttpClientFactory ClientFactory { get; }
        private IMapper Mapper { get; }
        public GiftsController(IHttpClientFactory clientFactory, IMapper mapper)
        {
            ClientFactory = clientFactory;
            Mapper = mapper;
        }

        // GET: /<controller>/
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
        public async Task<IActionResult> ViewGifts(int id)
        {
            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                ViewBag.Gifts = await secretSantaClient.GetGiftForUserAsync(id);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, GiftInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        viewModel.UserId = id;
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.AddGiftToUserAsync(id, Mapper.Map<GiftViewModel>(viewModel));

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GiftViewModel findGift = null;

            using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
            {
                try
                {
                    var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                    findGift = await secretSantaClient.GetGiftAsync(id);
                }
                catch (SwaggerException se)
                {
                    ModelState.AddModelError("", se.Message);
                }
            }

            return View(findGift);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, GiftInputViewModel viewModel)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        await secretSantaClient.EditGiftForUserAsync(id, viewModel);

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


        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = View();

            if (ModelState.IsValid)
            {
                using (var httpClient = ClientFactory.CreateClient("SecretSantaApi"))
                {
                    try
                    {
                        var secretSantaClient = new SecretSantaClient(httpClient.BaseAddress.ToString(), httpClient);
                        GiftViewModel viewModel = await secretSantaClient.GetGiftAsync(id);
                        await secretSantaClient.RemoveGiftFromUserAsync(viewModel.UserId.Value, viewModel.Id);

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
