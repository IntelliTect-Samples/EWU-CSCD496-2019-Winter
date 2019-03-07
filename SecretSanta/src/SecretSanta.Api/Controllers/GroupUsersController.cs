using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Error($"{nameof(groupId)} is not valid, returning bad request | 400");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Error($"{nameof(userId)} is not valid, returning bad request | 400");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Information($"{nameof(userId)} was added to {nameof(groupId)}, returning ok | 200");
                return Ok();
            }

            Log.Logger.Warning($"{nameof(userId)} or {nameof(groupId)} was not found, returing not found | 404");
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Logger.Error($"{nameof(groupId)} is not valid, returning bad request | 400");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Logger.Error($"{nameof(userId)} is not valid, returning bad request | 400");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Information($"{nameof(userId)} was removed from {nameof(groupId)}, returning ok | 200");
                return Ok();
            }

            Log.Logger.Warning($"{nameof(userId)} or {nameof(groupId)} was not found, returing not found | 404");
            return NotFound();
        }
    }
}
