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
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Domain.Models.Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpPost("{userid, title, decription}")]
        public CreatedResult PostGiftToUser(int userId, Domain.Models.Gift gift)
        {

            DTO.Gift returnGift = new DTO.Gift(gift);
            returnGift.UserID = userId;
            return Created($"api/gift/{gift.UserId}", returnGift);
        }
    }
}
