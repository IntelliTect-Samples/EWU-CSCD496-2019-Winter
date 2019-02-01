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

        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0) return NotFound();

            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);
            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpPost("{userId}")]
        public ActionResult AddGiftToUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0) return NotFound();
            if (gift is null) return BadRequest();

            Gift databaseGift = DTO.Gift.ToEntity(gift);

            _GiftService.AddGiftToUser(databaseGift, userId);
            return Ok();
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateGiftForUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0) return NotFound();
            if (gift is null) return BadRequest();

            Gift databaseGift = DTO.Gift.ToEntity(gift);
            _GiftService.UpdateGiftForUser(databaseGift, userId);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteGiftFromUser(DTO.Gift gift, int userId)
        {
            if (userId <= 0) return NotFound();
            if (gift is null) return BadRequest();

            Gift databaseGift = DTO.Gift.ToEntity(gift);
            _GiftService.DeleteGiftFromUser(databaseGift, userId);

            return Ok();
        }
    }
}
