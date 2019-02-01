using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;
        
        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet]
        public ActionResult<List<DTO.Group>> GetAllGroups()
        {
            return _GroupService.GetAllGroups().Select(x => new DTO.Group(x)).ToList();
        }

        [HttpPost]
        public ActionResult CreateGroup(DTO.Group group)
        {
            if (group is null) return BadRequest();
            if (group.Id != 0) group.Id = 0;

            var databaseGroup = DTO.Group.ToEntity(group);
            _GroupService.CreateGroup(databaseGroup);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateGroup(DTO.Group group)
        {
            if (group.Id <= 0) return NotFound();
            if (group is null) return BadRequest();

            Group databaseGroup = DTO.Group.ToEntity(group);
            _GroupService.UpdateGroup(databaseGroup);
            return Ok();
        }

        [HttpDelete("{groupId}")]
        public ActionResult DeleteGroup(int groupId)
        {
            if (groupId <= 0) return NotFound("GroupId's must be greater than 0");
            _GroupService.DeleteGroup(groupId);
            return Ok();
        }

        [HttpPost("{groupId}")]
        public ActionResult AddUserToGroup(DTO.User user, int groupId)
        {
            if (groupId <= 0 || user.Id <= 0) return NotFound("GroupId's and UserId's must be greater than 0");
            if (user is null) return BadRequest("User cannot be null");

            User databaseUser = DTO.User.ToEntity(user);
            _GroupService.AddUserToGroup(databaseUser, groupId);
            return Ok();
        }

        [HttpDelete("{groupId}/{userId}")]
        public ActionResult RemoveUserFromGroup(int userId, int groupId)
        {
            if (groupId <= 0 || userId <= 0) return NotFound("GroupId's and UserId's must be greater than 0");

            _GroupService.RemoveUserFromGroup(groupId, userId);
            return Ok();
        }
    }
}
