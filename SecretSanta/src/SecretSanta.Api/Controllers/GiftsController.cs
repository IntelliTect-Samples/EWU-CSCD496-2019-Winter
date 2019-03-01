using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class GiftsController : ControllerBase
    {
        private IGiftService GiftService { get; }
        private IMapper Mapper { get; }

        public GiftsController(IGiftService giftService, IMapper mapper)
        {
            GiftService = giftService;
            Mapper = mapper;
        }

        // GET api/Gift/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }
            List<Gift> databaseUsers = await GiftService.GetGiftsForUser(userId);

            return Ok(databaseUsers.Select(x => Mapper.Map<GiftViewModel>(x)).ToList());
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddGiftToUser(int id, GiftViewModel viewModel)
        {
            if (viewModel is null) return BadRequest();

            Gift gift = await GiftService.AddGiftToUser(id, Mapper.Map<Gift>(viewModel));

            return CreatedAtAction(nameof(GetGiftForUser), new { id = gift.UserId }, Mapper.Map<GiftViewModel>(gift));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditGiftForUser(int id, GiftInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedGifts = await GiftService.GetGiftsForUser(id);
            if (fetchedGifts == null)
            {
                return NotFound();
            }

            Gift updatedGift = Mapper.Map<Gift>(viewModel);
            await GiftService.UpdateGiftForUser(id, updatedGift);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveGiftFromUser(int userId, int giftId)
        {
            if (userId <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            var gifts = await GiftService.GetGiftsForUser(userId);

            if(gifts == null)
            {
                return NotFound();
            }

            await GiftService.RemoveGift(userId, giftId);

            return Ok();
        }
    }
}
