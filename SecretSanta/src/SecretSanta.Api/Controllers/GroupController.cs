using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

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

        // POST api/Group/
        [HttpPost]
        public ActionResult<DTO.Group> AddGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            return new DTO.Group(_GroupService.AddGroup(DTO.Group.ToDomain(group)));
        }

        // PUT api/Group/
        [HttpPut]
        public ActionResult<DTO.Group> UpdateGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            return new DTO.Group(_GroupService.UpdateGroup(DTO.Group.ToDomain(group)));
        }

        // DELETE api/Group/
        [HttpDelete]
        public ActionResult<DTO.Group> DeleteGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            return new DTO.Group(_GroupService.DeleteGroup(DTO.Group.ToDomain(group)));
        }

        // POST api/Group/{groupId}
        [HttpPost("{groupId}")]
        public ActionResult<DTO.User> AddUserToGroup(int groupId, DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (groupId <= 0)
            {
                return NotFound();
            }

            Domain.Models.User userToAdd = _GroupService.AddUserToGroup(groupId, DTO.User.ToDomain(user));
            return new DTO.User(userToAdd);
        }

        // DELETE api/Group/{groupId}
        [HttpDelete("{groupId}")]
        public ActionResult<DTO.User> RemoveUserFromGroup(int groupId, DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (groupId <= 0)
            {
                return NotFound();
            }

            Domain.Models.User userToRemove = _GroupService.RemoveUserFromGroup(groupId, DTO.User.ToDomain(user));
            return new DTO.User(userToRemove);
        }

        // GET api/Group/
        [HttpGet]
        public ActionResult<List<DTO.Group>> QueryAllGroups()
        {
            List<Group> databaseGroups = _GroupService.FetchAll();

            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        // GET api/Group/{groupId}
        [HttpGet("{groupId}")]
        public ActionResult<List<DTO.User>> QueryAllUsersInGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }
            List<User> groupUsers = _GroupService.FetchAllUsersInGroup(groupId);

            return groupUsers.Select(x => new DTO.User(x)).ToList();
        }
    }
}