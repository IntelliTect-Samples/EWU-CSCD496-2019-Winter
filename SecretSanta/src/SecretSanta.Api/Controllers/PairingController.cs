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

        // POST api/Pairing/id
        [HttpPost("{groupId}")]
        [Produces(typeof(List<PairingViewModel>))]
        public async Task<IActionResult> PostGeneratePairings(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            List<PairingViewModel> pairings = (await PairingService.GeneratePairings(groupId))
                .Select(x => Mapper.Map<PairingViewModel>(x)).ToList();

            return Created($"api/Pairing/{groupId}", pairings);
        }
    }
}