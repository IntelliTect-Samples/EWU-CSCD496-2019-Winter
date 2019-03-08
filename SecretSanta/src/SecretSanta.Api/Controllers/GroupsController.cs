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
            var groups = await GroupService.FetchAll().ConfigureAwait(false);
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            var group = await GroupService.GetById(id).ConfigureAwait(false);
            if (group == null)
            {
                Log.Logger.Debug("id: " + id + " was invalid for GetGroup(id)");
                return NotFound();
            }
            Log.Logger.Information("Successful call to GetGift(id) with id: " + id);
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Debug("viewModel was null for CreateGroup(viewModel)");
                return BadRequest();
            }
            var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel)).ConfigureAwait(false);
            Log.Logger.Information("Successful call to CreateGroup(viewModel) for viewModel: " + viewModel);
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Debug("viewModel was null for UpdateGroup(id, viewModel)");
                return BadRequest();
            }
            var group = await GroupService.GetById(id).ConfigureAwait(false);
            if (group == null)
            {
                Log.Logger.Debug("id: " + id + " was an invalid group id for UpdateGroup(id, viewModel)");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group).ConfigureAwait(false);
            Log.Logger.Information("");
            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Debug("id :" + id + " was an invalid group id for DeleteGroup(id)");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id).ConfigureAwait(false) )
            {
                Log.Logger.Information("Successful call to DeleteGroup(id) for id: " + id);
                return Ok();
            }
            Log.Logger.Debug("id: " + id + " was an invalid group id for DeleteGroup(id)");
            return NotFound();
        }
    }
}
