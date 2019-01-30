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

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        // POST api/Gift/5
        [HttpPost("{id}")]
        public ActionResult MakeGift(int id, string title)
        {
            if (title == null) return BadRequest();

            _GiftService.CreateGift(id, title);

            return Ok();
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
                return BadRequest();
            }

            _GiftService.EditGift(userID, DTO.Gift.GetDomainGift(upDatedGift));

            return Ok();
        }

        // DELETE api/Gift/5
        [HttpDelete("{uid}")]
        public ActionResult DeleteGift(int userId, int giftId)
        {
            if(userId <= 0 || giftId <= 0)
            {
                return NotFound();
            }

            _GiftService.DeleteGift(userId, giftId);

            return Ok();
        }
    }
}
