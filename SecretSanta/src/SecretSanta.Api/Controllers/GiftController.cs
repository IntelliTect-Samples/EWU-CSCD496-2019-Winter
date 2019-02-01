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
        [HttpPut]
        public ActionResult AddGiftToUser(int userId, DTO.Gift gift)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.AddGiftToUser(userId, DTO.Gift.ToDomain(gift));
            return Ok("gift successfully added");
        }

        [HttpDelete]
        public ActionResult RemoveGift(DTO.Gift gift)
        {
            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.RemoveGift(DTO.Gift.ToDomain(gift));
            return Ok("gift successfully removed");
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateGiftForUser(int userId, DTO.Gift gift)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            if (gift == null)
            {
                return BadRequest();
            }

            _GiftService.UpdateGiftForUser(userId, DTO.Gift.ToDomain(gift));
            return Ok("gift successfully updated");
        }
    }
}
