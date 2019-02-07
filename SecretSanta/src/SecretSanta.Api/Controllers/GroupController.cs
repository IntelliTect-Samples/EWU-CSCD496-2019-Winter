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
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            Mapper = mapper;
            GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/group
        [HttpGet]
        [Produces(typeof(IEnumerable<GroupViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllGroups()
        {
            //return new ActionResult<IEnumerable<GroupViewModel>>(GroupService.FetchAll().Select(x => GroupViewModel.ToViewModel(x)));
            return Ok(GroupService.FetchAll().Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        // GET api/group/5
        [HttpGet("{id}")]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGroup(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GroupViewModel>(GroupService.Find(id)));
        }

        // GET api/group/users/5
        [HttpGet("users/{groupId}")]
        [Produces(typeof(List<UserViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllUsersFromGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            if (GroupService.Find(groupId) == null)
            {
                return NotFound();
            }

            return Ok(GroupService.GetUsers(groupId).Select(x => Mapper.Map<UserViewModel>(x)));
        }

        // POST api/group
        [HttpPost]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            Group newGroup = GroupService.AddGroup(Mapper.Map<Group>(viewModel));

            if (newGroup == null)
            {
                return Conflict("Group: " + viewModel.Name + " is already in the database");
            }

            return CreatedAtAction(nameof(GetGroup), new { id = newGroup.Id }, Mapper.Map<GroupViewModel>(newGroup));
        }

        // PUT api/group/5
        [HttpPut("{id}")]
        [Produces(typeof(GroupViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedGroup = GroupService.Find(id);
            if (fetchedGroup == null)
            {
                return NotFound();
            }

            fetchedGroup.Name = viewModel.Name;

            return Ok(GroupService.UpdateGroup(fetchedGroup));
        }

        // PUT /api/Group/add/5
        [HttpPut("add/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (GroupService.AddUserToGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGroup(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A group id must be specified");
            }

            if (GroupService.DeleteGroup(id))
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/group/remove/5
        [HttpDelete("remove/{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (GroupService.RemoveUserFromGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
