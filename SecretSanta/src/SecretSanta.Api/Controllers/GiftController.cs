using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private IGiftService GiftService { get; }

        public GiftController(IGiftService giftService)
        {
            GiftService = giftService;
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<GiftViewModel>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => GiftViewModel.ToViewModel(x)).ToList();
        }
    }
}
