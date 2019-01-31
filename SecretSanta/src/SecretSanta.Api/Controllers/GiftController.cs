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

        private Gift GiftDtoToEntity(DTO.Gift dtoGift)
        {
            Gift entity = new Gift//same arguements as constructor
            {
                Id = dtoGift.Id,
                Title = dtoGift.Title,
                Description = dtoGift.Description,
                OrderOfImportance = dtoGift.OrderOfImportance,
                Url = dtoGift.Url
            };

            return entity;
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        public ActionResult<List<DTO.Gift>> GetGiftForUser(int dtoUserId)
        {
            if (IsValidId(dtoUserId))
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
            if (IsValidId(dtoUserId))
            {
                return NotFound();
            }

            if (IsNull(dtoGift))
            {
                return BadRequest();
            }

            _GiftService.AddGiftToUser(dtoUserId, GiftDtoToEntity(dtoGift));
            return Ok("Gift added!");

            //return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpDelete("{gift}")]
        public ActionResult DeleteGiftFromUser(DTO.Gift dtoGift)
        {
            if (IsNull(dtoGift))
            {
                return BadRequest();
            }
            _GiftService.RemoveGift(GiftDtoToEntity(dtoGift));

            return Ok("Gift removed!");
        }

        [HttpPost("{userId, gift}")]
        public ActionResult UpdateGiftFromUser(int dtoUserId, DTO.Gift dtoGift)//Update
        {
            if (IsValidId(dtoUserId))
            {
                return NotFound();
            }

            if (IsNull(dtoGift))
            {
                return BadRequest();
            }

            Domain.Models.Gift originalGift = GiftDtoToEntity(dtoGift);
            Domain.Models.Gift updateGift = _GiftService.UpdateGiftForUser(dtoUserId, GiftDtoToEntity(dtoGift));

            //?Check if gift is updated here or in test?

            return Ok("Gift updated!");
        }
    }
}