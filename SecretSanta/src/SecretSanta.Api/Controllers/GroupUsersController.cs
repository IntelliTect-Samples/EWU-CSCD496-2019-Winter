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
                Log.Logger.Debug("groupId: " + groupId + " was invalid for AddUserToGroup(groupId, userId)");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Debug("userId: " + userId + " was invalid for AddUserToGroup(groupId, userId)");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Information("Successfull call to GetGift(groupId, userId)");
                return Ok();
            }
            Log.Logger.Debug("userId: " + userId + ", or groupId: " + groupId + " was invalid for AddUserToGroup(groupId, userId)");
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Debug("groupId: " + groupId + " was invalid for RemoveUserFromGroup(groupId, userId)");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Debug("userId: " + userId + " was invalid for RemoveUserFromGroup(groupId, userId)");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Information("Successfull call to RemoveUserFromGroup(groupId, userId)");
                return Ok();
            }
            Log.Logger.Debug("userId: " + userId + ", or groupId: " + groupId + " was invalid for RemoveUserFromGroup(groupId, userId)");
            return NotFound();
        }
    }
}
