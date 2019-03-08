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
            var groups = await GroupService.FetchAll().ConfigureAwait(true);
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await GroupService.GetById(id).ConfigureAwait(true);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} was not found in call to {nameof(GroupService.GetById)}");
                return NotFound();
            }

            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} was passed in as null in {nameof(GroupsController.CreateGroup)}");
                return BadRequest();
            }
            Group createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel)).ConfigureAwait(true);
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} was passed in as null in {nameof(GroupsController.CreateGroup)}");
                return BadRequest();
            }
            var group = await GroupService.GetById(id).ConfigureAwait(true);
            if (group == null)
            {
                Log.Logger.Warning($"UserId {nameof(id)} was not found in call to {nameof(GroupService.GetById)}");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group).ConfigureAwait(true);

            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Warning($"{nameof(id)} was not greater than 0 as in {nameof(GroupsController.DeleteGroup)}");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id).ConfigureAwait(true))
            {
                Log.Logger.Verbose($"{nameof(id)} was successfully deleted in call to {nameof(GroupService.DeleteGroup)}");
                return Ok();
            }
            return NotFound();
        }
    }
}
