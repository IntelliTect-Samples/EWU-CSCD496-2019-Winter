using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

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

        [HttpGet]
        public async Task<ActionResult<GiftViewModel>> GetGift(int id)
        {
            var gift = await GiftService.GetGift(id);

            if (gift == null)
            {
                Log.Logger.Warning($"{nameof(gift)} not found in database. Returning NotFound");
                return NotFound();
            }

            Log.Logger.Information($"{nameof(gift)} successfully found in database. Returning Ok");
            return Ok(Mapper.Map<GiftViewModel>(gift));
        }

        [HttpPost]
        public async Task<ActionResult<GiftViewModel>> CreateGift(GiftInputViewModel viewModel)
        {
            var createdGift = await GiftService.AddGift(Mapper.Map<Gift>(viewModel));
            
            Log.Logger.Information($"{nameof(createdGift)} successfully created. Returning CreatedAtAction");

            return CreatedAtAction(nameof(GetGift), new { id = createdGift.Id }, Mapper.Map<GiftViewModel>(createdGift));
        }

        // GET api/Gift/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<GiftViewModel>>> GetGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                Log.Logger.Warning($"{nameof(userId)} must be specified and greater than 0. Returning NotFound");
                return NotFound();
            }
            List<Gift> databaseUsers = await GiftService.GetGiftsForUser(userId);
            
            Log.Logger.Information($"{nameof(databaseUsers)} successfully found from database. Returning Ok");

            return Ok(databaseUsers.Select(x => Mapper.Map<GiftViewModel>(x)).ToList());
        }
    }
}
