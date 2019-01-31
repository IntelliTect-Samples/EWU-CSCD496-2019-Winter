using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET api/Group/5
        [HttpGet("{groupId}")]
        public ActionResult<List<DTO.User>> GetUsersFromGroup(int groupId)
        {
            if(groupId <= 0)
            {
                return NotFound();
            }

            List<User> databaseGroup = _GroupService.GetUsersFromGroup(groupId);

            return databaseGroup.Select(x => new DTO.User(x)).ToList();
        }

        // GET api/Group
        [HttpGet]
        public ActionResult<List<DTO.Group>> GetAllGroups()
        {
            List<Group> databaseGroups = _GroupService.GetAllGroups();

            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        // DELETE api/Group/uid
        [HttpDelete("{groupId}")]
        public ActionResult DeleteGroup(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            if(_GroupService.DeleteGroup(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Delete error");
            }
        }

        // DELETE api/Group/remove/gid
        [HttpDelete("remove/{groupId}")]
        public ActionResult RemoveUserFromGroup(int gid, int uid)
        {
            if (gid <= 0 || uid <= 0)
            {
                return NotFound();
            }

            if(_GroupService.RemoveUser(uid, gid))
            {
                return Ok();
            }
            else
            {
                return BadRequest("User or Group not found");
            }
        }

        // POST api/group
        [HttpPost()]
        public ActionResult MakeGroup(string title)
        {
            if (title == null) return BadRequest();

            if (_GroupService.CreateGroup(title))
            { 
                return Ok();
            }
            else
            {
                return BadRequest("Create error");
            }
        }

        // PUT api/Group/gid
        [HttpPut("{groupId}")]
        public ActionResult UpdateGroup(int gid, DTO.Group upDataGroup)
        {
            if (gid <= 0)
            {
                return NotFound();
            }

            if(upDataGroup == null)
            {
                return BadRequest("Bad data packet");
            }

            if (_GroupService.FindGroup(gid) == null)
                return NotFound("Group not found");

            if (_GroupService.UpdateGroup(DTO.Group.GetDomainGroup(upDataGroup)))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Update Error");
            }
        }

        // PUT api/Group/add/5
        [HttpPut("add/{groupId}")]
        public ActionResult AddUserToGroup(int gid, int uid)
        {
            if(gid <= 0 || uid <= 0)
            {
                return NotFound();
            }

            if (_GroupService.AddUser(uid, gid))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Add error");
            }
        }
    }
}
