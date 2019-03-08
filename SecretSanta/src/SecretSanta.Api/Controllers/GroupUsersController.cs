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
                Log.Logger.Warning($"{nameof(groupId)} was not greater than 0 in {nameof(GroupUsersController.AddUserToGroup)}. Bad Request returned.");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"{nameof(userId)} was not greater than 0 in {nameof(GroupUsersController.AddUserToGroup)}. Bad Request returned");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Verbose($"User #{nameof(userId)} was placed in Group #{nameof(groupId)} successfully in call to {nameof(GroupService.AddUserToGroup)}");
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Warning($"groupId {nameof(groupId)} was not greater than 0 in {nameof(GroupUsersController.RemoveUserFromGroup)}. Bad Request returned.");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Warning($"userId {nameof(userId)} was not greater than 0 in {nameof(GroupUsersController.RemoveUserFromGroup)}. Bad Request returned.");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Verbose($"User #{nameof(userId)} was successfully removed from Group #{nameof(groupId)} in call to {nameof(GroupService.RemoveUserFromGroup)}");
                return Ok();
            }
            return NotFound();
        }
    }
}
