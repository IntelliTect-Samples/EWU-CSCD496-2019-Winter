using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            Log.Logger.Debug($"Properties inilized via constructor paramaters: GiftService = {nameof(giftService)}, Mapper = {nameof(mapper)}");
            GiftService = giftService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GiftViewModel>> GetGift(int id)
        {
            Log.Logger.Debug($"Getting Gift by id: {id}");
            var gift = await GiftService.GetGift(id).ConfigureAwait(false);

            if (gift == null)
            {
                Log.Logger.Warning($"GetGift by id not found, id = {id}");
                return NotFound();
            }

            Log.Logger.Information($"Getting Gift by id found and Gift returned, id = {id} Gift = {nameof(gift)}");
            return Ok(Mapper.Map<GiftViewModel>(gift));
        }

        [HttpPost]
        public async Task<ActionResult<GiftViewModel>> CreateGift(GiftInputViewModel viewModel)
        {
            Log.Logger.Information($"Creating new Gift object via AddGift method using passed in viewModel, viewMode = {viewModel.ToString()}");
            var createdGift = await GiftService.AddGift(Mapper.Map<Gift>(viewModel)).ConfigureAwait(false);

            Log.Logger.Information($"Returned GiftViewModel object, Gift = {createdGift.ToString()}");
            return CreatedAtAction(nameof(GetGift), new { id = createdGift.Id }, Mapper.Map<GiftViewModel>(createdGift));
        }

        // GET api/Gift/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ICollection<GiftViewModel>>> GetGiftsForUser(int userId)
        {
            if (userId <= 0)
            {
                Log.Logger.Warning($"Invalid userId passed in to search for Gift for User. Id must be greater than 0. Id = {userId}");
                return NotFound();
            }

            Log.Logger.Information($"Valid userId being used to retrieve list of Gifts for that userId. Id = {userId}");
            List<Gift> databaseUsers = await GiftService.GetGiftsForUser(userId).ConfigureAwait(false);

            Log.Logger.Information("Returned OK Request, validating List of Users properly mapped.");
            return Ok(databaseUsers.Select(x => Mapper.Map<GiftViewModel>(x)).ToList());
        }
    }
}
