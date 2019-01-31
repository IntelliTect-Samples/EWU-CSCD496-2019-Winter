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

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(userId);

            if (databaseUsers != null)
            {
                return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
            }
            else
            {
                return BadRequest("Get error");
            }
        }

        // POST api/Gift/5
        [HttpPost("{id}")]
        public ActionResult MakeGift(int id, DTO.Gift newGift)
        {
            if (newGift == null) return BadRequest();

            if (_GiftService.CreateGift(id, DTO.Gift.GetDomainGift(newGift)))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Make Gift error");
            }
        }

        // PUT api/Gift/id
        [HttpPut("{id}")]
        public ActionResult UpdateGiftForUser(int userID, DTO.Gift upDatedGift)
        {
            if (userID <= 0)
            {
                return NotFound();
            }

            if(upDatedGift == null)
            {
                return BadRequest("Bad data packet");
            }

            if (_GiftService.EditGift(userID, DTO.Gift.GetDomainGift(upDatedGift)))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Edit gift error");
            }
        }

        // DELETE api/Gift/5
        [HttpDelete("{uid}")]
        public ActionResult DeleteGift(int userId, int giftId)
        {
            if(userId <= 0 || giftId <= 0)
            {
                return NotFound();
            }

            if (_GiftService.DeleteGift(userId, giftId))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Delete gift error");
            }
        }
    }
}
