using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : ControllerBase
    {
        private IPairingService PairingService { get; }
        private IMapper Mapper { get; }

        public PairingsController(IPairingService pairingService, IMapper mapper)
        {
            PairingService = pairingService;
            Mapper = mapper;
        }

        // POST api/Pairings
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> GeneratePairings(int groupId)
        {
            if (groupId < 1)
            {
                return BadRequest("A group id must be specified");
            }

            List<Pairing> result = await PairingService.GeneratePairings(groupId);
            if (result.Count == 0) return BadRequest();

            return Created($"/pairing/{groupId}", result.Select(p => Mapper.Map<PairingViewModel>(p)).ToList());
        }

    }
}