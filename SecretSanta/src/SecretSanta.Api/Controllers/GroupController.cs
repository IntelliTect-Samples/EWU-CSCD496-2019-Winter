using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // Create group
        [HttpPost()] // AddGroup
        public ActionResult<DTO.Group> CreateGroup([FromBody] DTO.Group @group)
        {
            if (@group == null)
            {
                return BadRequest();
            }

            var addedGroup = _groupService.AddGroup(DTO.Group.ToEntity(group));

            return Ok(new DTO.Group(addedGroup));
        }

        // Update group
        [HttpPut()] // UpdateGroup
        public ActionResult<DTO.Group> UpdateGroup([FromBody] DTO.Group @group)
        {
            if (@group == null)
            {
                return BadRequest();
            }

            var updatedGroup = _groupService.UpdateGroup(DTO.Group.ToEntity(group));

            return Ok(new DTO.Group(updatedGroup));
        }

        // Delete group
        [HttpDelete()] // DeleteGroup
        public ActionResult DeleteGroup([FromBody] DTO.Group @group)
        {
            if (@group == null)
            {
                return BadRequest();
            }
            
            _groupService.DeleteGroup(DTO.Group.ToEntity(group));

            return Ok();
        }

        // Add user to Group
        [HttpPost("{groupId}")] // AddUserToGroup
        public ActionResult<DTO.User> AddUserToGroup(int groupId, [FromBody] DTO.User user)
        {
            if (groupId <= 0)
            {
                return NotFound(groupId);
            }

            if (user == null)
            {
                return BadRequest();
            }

            Domain.Models.User addedUser = _groupService.AddUserToGroup(groupId, DTO.User.ToEntity(user));

            return Ok(new DTO.User(addedUser));
        }

        // Remove user from group
        [HttpDelete("{groupId}")] // RemoveUserFromGroup
        public ActionResult<DTO.User> RemoveUserFromGroup(int groupId, [FromBody] DTO.User user)
        {
            if (groupId <= 0)
            {
                return NotFound(groupId);
            }

            if (user == null)
            {
                return BadRequest();
            }

            Domain.Models.User removedUser = _groupService.RemoveUserFromGroup(groupId, DTO.User.ToEntity(user));

            return Ok(new DTO.User(removedUser));
        }

        // Get all groups
        [HttpGet()] // FetchAll
        public ActionResult<List<DTO.Group>> FetchAll()
        {
            List<Domain.Models.Group> groups = _groupService.FetchAll();

            return groups.Select(group => new DTO.Group(group)).ToList();
        }
        

        // Get users from groups
        [HttpGet("{groupId}")] // GetUsers
        public ActionResult<List<DTO.User>> GetUsers(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound(groupId);
            }

            List<Domain.Models.User> users = _groupService.GetUsers(groupId);

            return users.Select(user => new DTO.User(user)).ToList();
        }
    }
}