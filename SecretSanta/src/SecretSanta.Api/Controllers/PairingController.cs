using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingController : ControllerBase
    {
        private IPairingService PairingService { get; }
        private IMapper Mapper { get; }

        public PairingController(IPairingService pairingService, IMapper mapper)
        {
            PairingService = pairingService;
            Mapper = mapper;
        }

        // POST api/Pairing
        [HttpPost]
        [Produces(typeof(PairingViewModel))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(int groupId)
        {
            if (groupId < 1 )
            {
                return BadRequest();
            }
            List<Pairing> result = await PairingService.GeneratePairings(groupId);

            if (result is null)
            {
                return BadRequest();
            }
            return Ok(Mapper.Map<PairingViewModel>(result));
        }

    }
}
