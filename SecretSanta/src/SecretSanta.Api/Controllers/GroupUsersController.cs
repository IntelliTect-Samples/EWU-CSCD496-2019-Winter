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
                Log.Logger.Warning($"GroupId<0 in AddUserToGroup. Status: 400");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"UserId<0 in AddUserToGroup. Status: 400");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId))
            {
                Log.Logger.Information($"User successfully added to group. Status: 200");
                return Ok();
            }

            Log.Logger.Warning($"User failed to add to group. Status: 404");
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Warning($"GroupId<0 in RemoveUserFromGroup. Status: 400");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"UserId<0 in RemoveUserFromGroup. Status: 400");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId))
            {
                Log.Logger.Information($"User successfully removed from group. Status: 200");
                return Ok();
            }

            Log.Logger.Warning($"User failed to remove from group. Status: 404");
            return NotFound();
        }
    }
}
