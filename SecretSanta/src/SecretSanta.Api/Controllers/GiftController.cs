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
        [HttpGet("{dtoUserId}")]
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
        [HttpPost("{dtoGift, dtoUserId}")]
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

        [HttpDelete("{dtoGift}")]
        public ActionResult DeleteGiftFromUser(DTO.Gift dtoGift)
        {
            if (_HelperMethod.IsNull(dtoGift))
            {
                return BadRequest();
            }
            _GiftService.RemoveGift(DTO.Gift.ToEntity(dtoGift));

            return Ok("Gift removed!");
        }

        [HttpPost("{dtoUserId, dtoGift}")]
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

            _GiftService.UpdateGiftForUser(dtoUserId, DTO.Gift.ToEntity(dtoGift));

            return Ok("Gift updated!");
        }
    }
}