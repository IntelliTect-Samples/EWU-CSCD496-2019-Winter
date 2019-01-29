﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO;
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

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Domain.Models.Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpPost("{userid, gift}")]
        public ActionResult PostGiftToUser(int userId, Domain.Models.Gift gift)
        {
            if(gift == null || userId <= 0)
            {
                return BadRequest();
            }

            Domain.Models.Gift insertedGift = _GiftService.AddGiftToUser(userId, gift);

            DTO.Gift returnGift = new DTO.Gift(insertedGift);

            return Created($"api/gift/{gift.UserId}", returnGift);
        }

        [HttpPut("{userid, gift}")]
        public ActionResult PutUserGift(int userId, Domain.Models.Gift gift)
        {
            if (gift == null || userId <= 0)
            {
                return BadRequest();
            }

            Domain.Models.Gift updatedGift = _GiftService.UpdateGiftForUser(userId, gift);

            if (GiftsAreEqual(updatedGift, gift))
            {
                return Ok("Gift updated!");
            }
            return BadRequest();
        }

        private bool GiftsAreEqual(Domain.Models.Gift updatedGift, Domain.Models.Gift gift)
        {
            return (updatedGift.Description == gift.Description &&
                    updatedGift.Id == gift.Id &&
                    updatedGift.OrderOfImportance == gift.OrderOfImportance &&
                    updatedGift.Url == gift.Url &&
                    updatedGift.User == gift.User &&
                    updatedGift.UserId == gift.UserId);
        }

        [HttpDelete("{gift}")]
        public ActionResult DeleteGift(Domain.Models.Gift gift)
        {
            _GiftService.RemoveGift(gift);
            return Ok("Gift removed!");
        }
    }
}
