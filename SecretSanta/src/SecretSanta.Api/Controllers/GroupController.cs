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
        public IMapper Mapper { get; }

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/group
        [HttpGet]
        [Produces(typeof(ICollection<GroupViewModel>))]
        public IActionResult Get()
        {
            return Ok(GroupService.FetchAll().Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        // POST api/group
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPost]
        [Produces(typeof(ICollection<GroupViewModel>))]
        public IActionResult CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Passed in viewModel cannot be null");
            }
            else
            {
                var addedGroup = GroupService.AddGroup(Mapper.Map<Group>(viewModel));
                return CreatedAtAction(nameof(Get), new { id = addedGroup.Id }, Mapper.Map<GroupViewModel>(addedGroup));
                //return GroupViewModel.ToViewModel();
            }
        }

        // PUT api/group/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [HttpPut("{id}")]
        public IActionResult UpdateGroup(int userId, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Passed in GroupInputViewModel cannot be null");
            }
            if (userId <= 0)
            {
                return BadRequest($"User Id {userId} must greater than 0");
            }

            var fetchedGroup = GroupService.Find(userId);
            if (fetchedGroup == null)
            {
                return NotFound($"User {userId} not found");
            }

            //fetchedGroup.Name = viewModel.Name;
            else
            {
                Mapper.Map(viewModel, fetchedGroup);
                var updatedUser = GroupService.UpdateGroup(fetchedGroup);

                return Ok(updatedUser);
            }

            //return GroupViewModel.ToViewModel(GroupService.UpdateGroup(fetchedGroup));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [HttpPut("{groupId}/{userid}")]
        public IActionResult AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest($"Group Id {groupId} must must be greater than 0");
            }

            else if (userId <= 0)
            {
                return BadRequest($"User Id {userId} must be greater than 0");
            }

            else if (GroupService.AddUserToGroup(groupId, userId))
            {
                return Ok($"User Id {userId} was added to Group {groupId}");
            }

            else
            {
                return NotFound($"Group {groupId} not found");
            }
        }

        // DELETE api/group/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest($"A group id {groupId} must be greater than 0");
            }

            else if (GroupService.DeleteGroup(groupId))
            {
                return Ok($"Group {groupId} was deleted.");
            }

            else
            {
                return NotFound($"Group {groupId} not found");
            }
        }
    }
}
