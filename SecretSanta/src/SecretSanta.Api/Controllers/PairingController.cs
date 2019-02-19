using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingController : ControllerBase
    {
        private IPairingService PairingService { get; set; }
        private IMapper Mapper { get; set; }

        public PairingController(IPairingService pairingService, IMapper mapper)
        {
            PairingService = pairingService;
            Mapper = mapper;
        }

        [HttpGet("groupId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(ICollection<PairingViewModel>))]
        public async Task<IActionResult> GetPairingsForGroup(int groupId)
        {
            if (groupId <= 0) return BadRequest();
            List<Pairing> databasePairings = await PairingService.GetPairingsForGroup(groupId);

            if (databasePairings is null) return NotFound();
            return Ok(databasePairings.Select(p => Mapper.Map<PairingViewModel>(p)).ToList());
        }

        [HttpPost("groupId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(ICollection<PairingViewModel>))]
        public async Task<IActionResult> GeneratePairings(int groupId)
        {
            if (groupId <= 0) return BadRequest();
            List<Pairing> databasePairings = await PairingService.GeneratePairingsForGroup(groupId);

            if (databasePairings is null) return NotFound();
            return Ok(databasePairings.Select(p => Mapper.Map<PairingViewModel>(p)).ToList());
        }
    }
}