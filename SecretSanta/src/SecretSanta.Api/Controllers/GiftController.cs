using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            List<Domain.Models.Gift> databaseUsers = _giftService.GetGiftsForUser(userId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        // POST api/Gift/4
        [HttpPost("{userId}")] // Create
        public ActionResult<DTO.Gift> AddGiftToUser([FromBody] DTO.Gift gift, int userId)
        {
            if (userId <= 0)
            {
                return NotFound(gift);
            }

            if (gift == null)
            {
                return BadRequest();
            }

            Domain.Models.Gift addedGift = _giftService.AddGiftToUser(userId, DTO.Gift.ToEntity(gift));
            return Ok(new DTO.Gift(addedGift));
        }

        // PUT api/Gift/4
        [HttpPut("{userId}")] // Update
        public ActionResult<DTO.Gift> UpdateGiftForUser([FromBody] DTO.Gift gift, int userId)
        {
            if (userId <= 0)
            {
                return NotFound(gift);
            }

            if (gift == null)
            {
                return BadRequest();
            }

            Domain.Models.Gift updatedGift = _giftService.UpdateGiftForUser(userId, DTO.Gift.ToEntity(gift));
            return Ok(new DTO.Gift(updatedGift));
        }

        // PUT api/Gift/4
        [HttpDelete()] // Remove
        public ActionResult RemoveGiftFromUser([FromBody] DTO.Gift gift)
        {
            if (gift == null)
            {
                return BadRequest();
            }

            _giftService.RemoveGift(DTO.Gift.ToEntity(gift));

            return Ok();
        }
    }
}