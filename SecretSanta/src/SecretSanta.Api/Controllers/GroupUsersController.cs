using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

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
        public async Task<ActionResult> AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Warning($"{nameof(groupId)} must be specified and greater than 0. Returning BadRequest");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"{nameof(userId)} must be specified and greater than 0. Returning BadRequest");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId))
            {
                Log.Logger.Information($"Group with groupId of {groupId} and userId of {userId} added. Returning Ok");
                return Ok();
            }
            
            Log.Logger.Warning($"Group with groupId of {groupId} and userId {userId}. Returning NotFound");
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Warning($"{nameof(groupId)} must be specified and greater than 0. Returning BadRequest");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"{nameof(userId)} must be specified and greater than 0. Returning BadRequest");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId))
            {
                Log.Logger.Information($"Group with groupId of {groupId} and userId of {userId} successfully removed from database. Returning Ok");
                return Ok();
            }

            Log.Logger.Warning(
                $"Group with groupId of {groupId} and userId of {userId} could not be found in database. Returning NotFound");
            return NotFound();
        }
    }
}
