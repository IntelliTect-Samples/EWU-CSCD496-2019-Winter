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
    public class GroupsController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService;
            Mapper = mapper;
        }

        // GET api/group
        [HttpGet]
        public async Task<ActionResult<ICollection<GroupViewModel>>> GetGroups()
        {
            var groups = await GroupService.FetchAll();
            
            Log.Logger.Information($"All groups successfully fetched from database. Returning ok");
            
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} null on GetGroup. Returning NotFound");
                return NotFound();
            }

            Log.Logger.Information($"{nameof(group)} successfully found. Returning Ok");
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} null on CreateGroup. Returning BadRequest");
                return BadRequest();
            }
            
            var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel));
            Log.Logger.Warning($"{nameof(createdGroup)} successfully added to database. Returning CreatedAtAction");
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} null on UpdateGroup. Returning BadRequest");
                return BadRequest();
            }
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} null on UpdateGroup. Returning NotFound");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group);
            
            Log.Logger.Information("Group updated. Returning NoContent");

            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Warning($"{nameof(id)} must be specified and greater than 0. Returning BadRequest");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id))
            {
                Log.Logger.Warning($"Group with id of {id} successfully deleted. Returning Ok");
                return Ok();
            }
            
            Log.Logger.Warning($"Group with id of {id} could not be found. Returning NotFound");
            return NotFound();
        }
    }
}
