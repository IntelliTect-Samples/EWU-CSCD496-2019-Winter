using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecretSanta.Api.ViewModels;
using Microsoft.AspNetCore.Http;

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
        [Produces(typeof(ICollection<PairingViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPairingList(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest(); //400
            }

            else
            {
                List<Pairing> generatedPairings = await PairingService.GetPairingsList(groupId);

                if (generatedPairings == null)
                {
                    return NotFound(); //404
                }
                else
                {
                    return Ok(generatedPairings.Select(pair => Mapper.Map<PairingViewModel>(pair))
                                                                                           .ToList()); //200
                }
            }
        }

        [HttpPost("groupId")]
        [Produces(typeof(ICollection<PairingViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GeneratePairings(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest(); //400
            }
            else
            {
                List<Pairing> databasePairings = await PairingService.GeneratePairing(groupId);

                if (databasePairings == null)
                {
                    return NotFound(); //404
                }
                else
                {
                    return Ok(databasePairings.Select(p => Mapper.Map<PairingViewModel>(p))
                                                                                           .ToList()); //200
                }
            }
        }
    }
}
