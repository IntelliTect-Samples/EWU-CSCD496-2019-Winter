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

            Log.Logger.Information($"All groups successfully fetched. Status:200");
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} not found in GetGroup. Status: 404");
                return NotFound();
            }

            Log.Logger.Information($"{nameof(group)} successfully found. Status: 200");
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"Argument: {nameof(viewModel)} was null in CreateGroup. Status: 400");
                return BadRequest();
            }
            var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel));

            Log.Logger.Information($"{nameof(createdGroup)} added to database. Status: 201");
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"Argument: {nameof(viewModel)} was null in UpdateGroup. Status: 400");
                return BadRequest();
            }
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} not found in UpdateGroup. Status: 404");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group);

            Log.Logger.Information("Group updated. Status: 204");
            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Warning($"Id less than 0. Status: 400");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id))
            {
                Log.Logger.Information($"Group deleted. Status: 200");
                return Ok();
            }
            Log.Logger.Warning($"Group not found. Status: 404");
            return NotFound();
        }
    }
}
