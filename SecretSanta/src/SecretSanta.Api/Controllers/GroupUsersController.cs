using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Http;

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
                Log.Logger.Debug($"{nameof(groupId)} is not valid before function call {nameof(GroupService.AddUserToGroup)}. Bad Request.");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Debug($"{nameof(userId)} is not valid before function call {nameof(GroupService.AddUserToGroup)}. Bad Request.");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId))
            {
                Log.Logger.Information($"Added User To Group.", groupId, userId);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Debug($"{nameof(groupId)} is not valid before function call {nameof(GroupService.RemoveUserFromGroup)}. Bad Request.");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Debug($"{nameof(userId)} is not valid before function call {nameof(GroupService.RemoveUserFromGroup)}. Bad Request.");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId))
            {
                Log.Logger.Information($"Removed User From Group.", groupId, userId);
                return Ok();
            }
            return NotFound();
        }
    }
}
