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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get()
        {
            List<Group> fetchedAllGroups = await GroupService.FetchAll();
            return Ok(fetchedAllGroups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int id)
        {
            Group group = await GroupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Mapper.Map<GroupViewModel>(group));
            }
        }

        // POST api/group
        [HttpPost]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            else
            {
                Group createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel));
                return CreatedAtAction(nameof(Get), new { id = createdGroup.Id }, Mapper.Map<GroupViewModel>(createdGroup));
            }
        }

        // PUT api/group/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            else
            {
                Group group = await GroupService.GetById(id);
                if (group == null)
                {
                    return NotFound();
                }
                else
                {
                    Mapper.Map(viewModel, group);
                    Group result = await GroupService.UpdateGroup(group);

                    return NoContent();
                }
            }
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A group id must be specified");
            }
            else
            {
                bool isDeleted = await GroupService.DeleteGroup(id);
                if (isDeleted)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
