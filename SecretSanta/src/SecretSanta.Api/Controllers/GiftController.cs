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
        private IGiftService _GiftService;

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{dtoUserId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int dtoUserId)
        {
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(dtoUserId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPut]
        public ActionResult AddGiftToUser(DTO.Gift dtoGift, int dtoUserId)
        {
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            if (dtoGift == null)
            {
                return BadRequest();
            }

            _GiftService.AddGiftToUser(dtoUserId, DTO.Gift.ToEntity(dtoGift));
            return Ok("Gift added!");

        }

        [HttpDelete]
        public ActionResult DeleteGiftFromUser(DTO.Gift dtoGift, int dtoUserId)
        {
            if (dtoGift == null)
            {
                return BadRequest();
            }
            if (dtoUserId <= 0)
            {
                return NotFound();
            }
            _GiftService.RemoveGift(dtoUserId, DTO.Gift.ToEntity(dtoGift));

            return Ok("Gift removed!");
        }

        [HttpPut("{dtoUserId}")]
        public ActionResult UpdateGiftFromUser(int dtoUserId, DTO.Gift dtoGift)//Update
        {
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            if (dtoGift == null)
            {
                return BadRequest();
            }

            _GiftService.UpdateGiftForUser(dtoUserId, DTO.Gift.ToEntity(dtoGift));

            return Ok("Gift updated!");
        }
    }
}