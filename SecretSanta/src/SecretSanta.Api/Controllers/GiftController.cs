using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _GiftService;

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/#
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
            
        }

        // POST api/Gift/#
        [HttpPost("{userId}")]
        public ActionResult<DTO.Gift> CreateGift(int userId, DTO.Gift gift)
        {
            if (gift == null || userId <= 0)
            {
                return BadRequest();
            }

            return new DTO.Gift(_GiftService.AddGiftToUser(userId, DTO.Gift.ToDomain(gift)));
        }
    }
}
