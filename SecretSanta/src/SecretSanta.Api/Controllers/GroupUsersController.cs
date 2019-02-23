using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class GroupUsersController : ControllerBase
    {
        private IGroupService GroupService { get; }

        public GroupUsersController(IGroupService groupService)
        {
            GroupService = groupService;
        }

        [HttpPut("{groupId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }
            else if (userId <= 0)
            {
                return BadRequest();
            }
            else
            {
                bool isAdded = await GroupService.AddUserToGroup(groupId, userId);

                if (isAdded)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete("{groupId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }
            else if (userId <= 0)
            {
                return BadRequest();
            }
            else
            {
                bool isRemoved = await GroupService.RemoveUserFromGroup(groupId, userId);

                if (isRemoved)
                {
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
