using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

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

        //POST api/pairing/5
        [HttpPost]
        [Produces(typeof(PairingViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MakePairings(int groupId)
        {
            if(groupId <= 0)
            {
                Log.Logger.Error($"{nameof(groupId)} is not vaild, returing bad request | 400");
                return BadRequest("A group id must be specified");
            }

            if(await PairingService.GenerateAllPairs(groupId).ConfigureAwait(false))
            {
                Log.Logger.Information($"{nameof(groupId)} had all pairs generated, returning ok | 200");
                return Ok();
            }

            Log.Logger.Error($"{nameof(groupId)}, there was an error, returning bad request | 400");
            return BadRequest();
        }
    }
}