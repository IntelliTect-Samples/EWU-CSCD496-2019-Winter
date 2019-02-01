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

        // POST api/Gift/{userId}
        [HttpPost("{userId}")]
        public ActionResult<DTO.Gift> AddGiftToUser(int userId, DTO.Gift gift)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            if (gift == null)
            {
                return BadRequest();
            }

            return new DTO.Gift(_GiftService.AddGiftToUser(userId, DTO.Gift.ToDomain(gift)));
        }

        // PUT api/Gift/{userId}
        [HttpPut("{userId}")]
        public ActionResult<DTO.Gift> UpdateGiftForUser(int userId, DTO.Gift gift)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            if (gift == null)
            {
                return BadRequest();
            }

            return new DTO.Gift(_GiftService.UpdateGiftForUser(userId, DTO.Gift.ToDomain(gift)));
        }

        // DELETE api/Gift/
        [HttpDelete]
        public ActionResult RemoveGift(DTO.Gift gift)
        {
            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.RemoveGift(DTO.Gift.ToDomain(gift));
            return Ok();
        }

        // GET api/Gift/{userId}
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> QueryAllGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseGifts = _GiftService.GetGiftsForUser(userId);
            
            return databaseGifts.Select(x => new DTO.Gift(x)).ToList();
        }
    }
}