using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Http;

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
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Debug($"{nameof(group)} is null after function call {nameof(GroupService.GetById)}. Not Found.");

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
                Log.Logger.Debug($"{nameof(viewModel)} is null before function call {nameof(GroupService.AddGroup)}. Bad Request.");
                return BadRequest();
            }
            var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel));

            Log.Logger.Information($"Group created.", viewModel);
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Debug($"{nameof(viewModel)} is null before function call {nameof(GroupService.GetById)}. Bad Request.");
                return BadRequest();
            }
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Logger.Debug($"{nameof(group)} is null after function call {nameof(GroupService.GetById)}. Not Found.");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group);

            Log.Logger.Information($"Group Updated.", viewModel);
            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Debug($"{nameof(id)} is not valid before function call {nameof(GroupService.DeleteGroup)}. Bad Request.");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id))
            {
                Log.Logger.Information($"Group Deleted.", id);
                return Ok();
            }
            Log.Logger.Debug($"{nameof(id)} is not valid after function call {nameof(GroupService.DeleteGroup)}. Not Found.");
            return NotFound();
        }
    }
}
