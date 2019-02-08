using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private IGiftService GiftService { get; }
        private IMapper Mapper { get; }

        public GiftController(IGiftService giftService, IMapper mapper)
        {
            GiftService = giftService;
            Mapper = mapper;
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            List<Gift> databaseGifts = GiftService.GetGiftsForUser(userId);

            if (databaseGifts is null || databaseGifts.Count == 0) return NotFound();

            var viewModelGiftList = databaseGifts.Select(x => Mapper.Map<GiftViewModel>(x)).ToList();

            return Ok(viewModelGiftList);
        }

        // POST api/Gift/userId
        [HttpPost("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddGiftToUser(int userId, GiftInputViewModel giftInputViewModel)
        {
            if (giftInputViewModel is null || userId <= 0) return BadRequest();

            var persistedGift = GiftService.AddGiftToUser(userId, Mapper.Map<Gift>(giftInputViewModel));

            if (persistedGift is null) return NotFound();

            return Ok(Mapper.Map<GiftViewModel>(persistedGift));
        }

        // PUT api/Gift/userId
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateGiftForUser(int userId, GiftInputViewModel giftInputViewModel)
        {
            if (giftInputViewModel is null || userId <= 0) return BadRequest();

            var persistedGift = GiftService.UpdateGiftForUser(userId, Mapper.Map<Gift>(giftInputViewModel));

            if (persistedGift is null) return NotFound();

            return Ok(Mapper.Map<GiftViewModel>(persistedGift));
        }

        // DELETE api/Gift/giftId
        [HttpDelete("{giftId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int giftId)
        {
            bool giftWasDeleted = GiftService.RemoveGift(giftId);

            if (!giftWasDeleted) return NotFound();

            return Ok();
        }
    }
}
