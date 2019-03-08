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
            Log.Logger.Debug($"Properties inilized via constructor paramaters: IGroupService = {nameof(groupService)}");
            GroupService = groupService;
        }

        [HttpPut("{groupId}")]
        public async Task<ActionResult> AddUserToGroup(int groupId, int userId)
        {
            StatusCodeResult statusCodeResult = null;

            if (groupId <= 0)
            {
                Log.Logger.Warning($"Invalid group ID passed in to search for Gift for User. Id must be greater than 0. Group ID = {groupId}");
                statusCodeResult = BadRequest();
            }

            else if (userId <= 0)
            {
                Log.Logger.Warning($"Invalid user ID passed in to search for Gift for User. Id must be greater than 0. User ID = {userId}");
                statusCodeResult = BadRequest();
            }

            else if (await GroupService.AddUserToGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Debug($"Valid userId and Valid groupId. Added user to group. User Id = {userId}, Group Id = {groupId}");
                statusCodeResult = Ok();
            }
            else
            {
                statusCodeResult = NotFound();
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            StatusCodeResult statusCodeResult = null; //will be reassigned regardless, edited code to meet Mark's guidelines of having one return

            if (groupId <= 0)
            {
                Log.Logger.Warning($"Invalid group ID passed in to search for Gift for User. Id must be greater than 0. Group ID = {groupId}");
                statusCodeResult = BadRequest();
            }

            else if (userId <= 0)
            {
                Log.Logger.Warning($"Invalid user ID passed in to search for User. Id must be greater than 0. User ID = {userId}");
                statusCodeResult = BadRequest();
            }

            else if (await GroupService.RemoveUserFromGroup(groupId, userId).ConfigureAwait(false))
            {
                Log.Logger.Debug($"Valid userId and Valid groupId. Removed user from group. User Id = {userId}, Group Id = {groupId}");
                statusCodeResult = Ok();
            }

            else
            {
                Log.Logger.Warning("User was not found, user was not removed from group");
                statusCodeResult = NotFound();
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }
    }
}
