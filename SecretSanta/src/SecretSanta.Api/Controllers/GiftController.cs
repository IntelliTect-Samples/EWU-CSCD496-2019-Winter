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
        private readonly HelperControllerMethods _HelperMethod = new HelperControllerMethods();

        public GiftController(IGiftService giftService)
        {
            _GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int dtoUserId)
        {
            if (_HelperMethod.IsValidId(dtoUserId))
            {
                return NotFound();
            }
            List<Gift> databaseUsers = _GiftService.GetGiftsForUser(dtoUserId);

            return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{userId}")]
        public ActionResult AddGiftToUser(DTO.Gift dtoGift, int dtoUserId)
        {
            if (_HelperMethod.IsValidId(dtoUserId))
            {
                return NotFound();
            }

            if (_HelperMethod.IsNull(dtoGift))
            {
                return BadRequest();
            }

            _GiftService.AddGiftToUser(dtoUserId, DTO.Gift.ToEntity(dtoGift));
            return Ok("Gift added!");

        }

        [HttpDelete("{gift}")]
        public ActionResult DeleteGiftFromUser(DTO.Gift dtoGift)
        {
            if (_HelperMethod.IsNull(dtoGift))
            {
                return BadRequest();
            }
            _GiftService.RemoveGift(DTO.Gift.ToEntity(dtoGift));

            return Ok("Gift removed!");
        }

        [HttpPost("{userId, gift}")]
        public ActionResult UpdateGiftFromUser(int dtoUserId, DTO.Gift dtoGift)//Update
        {
            if (_HelperMethod.IsValidId(dtoUserId))
            {
                return NotFound();
            }

            if (_HelperMethod.IsNull(dtoGift))
            {
                return BadRequest();
            }

            Domain.Models.Gift originalGift = DTO.Gift.ToEntity(dtoGift);
            Domain.Models.Gift updateGift = _GiftService.UpdateGiftForUser(dtoUserId, DTO.Gift.ToEntity(dtoGift));

            //?Check if gift is updated here or in test?

            return Ok("Gift updated!");
        }
    }
}