using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        [Produces(typeof(ICollection<GroupViewModel>))]
        [ProducesResponseType(typeof(ICollection<GroupViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGroups()
        {
            Log.Logger.Information($"returning all groups | 200");
            return Ok((await GroupService.FetchAll().ConfigureAwait(false)).Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GroupViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroup(int id)
        {
            Group group = await GroupService.GetById(id).ConfigureAwait(false);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} was not found, returning not found | 404");
                return NotFound();
            }

            Log.Logger.Warning($"{nameof(group)} was found, returning ok | 200");
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(typeof(GroupViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Error($"{nameof(viewModel)} was null, returning bad request | 400");
                return BadRequest();
            }
            Group createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel)).ConfigureAwait(false);
            Log.Logger.Information($"{nameof(viewModel)} was created, returning URL of said group | 201");
            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Error($"{nameof(viewModel)} was null, returing bad request | 400");
                return BadRequest();
            }
            Group group = await GroupService.GetById(id).ConfigureAwait(false);
            if (group == null)
            {
                Log.Logger.Warning($"{nameof(group)} was not found, returing not found | 404");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group).ConfigureAwait(false);
            Log.Logger.Information($"{nameof(group)} was updated, returing no content | 204");
            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Error($"{nameof(id)} was not valid, returning bad request | 400");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id).ConfigureAwait(false))
            {
                Log.Logger.Information($"{nameof(id)} was deleted, returning ok | 200");
                return Ok();
            }

            Log.Logger.Warning($"{nameof(id)} was not found, returning not found | 404");
            return NotFound();
        }
    }
}
