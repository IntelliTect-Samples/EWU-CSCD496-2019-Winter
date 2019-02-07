using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System;

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
            GiftService = giftService ?? throw new ArgumentNullException(nameof(giftService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/Gift/5
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetGiftForUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest($"User id {userId} msust be greater than 0");
            }
            else
            {
                List<Gift> databaseUsers = GiftService.GetGiftsForUser(userId);

                var foundGiftsForUser = databaseUsers.Select(x => Mapper.Map<GiftViewModel>(x)).ToList();
                return Ok(foundGiftsForUser);
            }
        }
    }
}
