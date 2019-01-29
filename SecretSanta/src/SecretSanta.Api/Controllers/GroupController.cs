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
        public ActionResult<bool> DeleteGroup(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            return _GroupService.DeleteGroup(id);
        }

        // POST api/group/title
        [HttpPost("{title}")]
        public ActionResult<bool> MakeGroup(string title)
        {
            if (title == null) return false;

            return _GroupService.CreateGroup(title);
        }

        // PUT api/Group/gid/uid
        [HttpPut("{groupId/userId}")]
        public ActionResult<bool> AddUserToGroup(int gid, int uid)
        {
            if(gid <= 0 || uid <= 0)
            {
                return NotFound();
            }

            return _GroupService.AddUser(uid, gid);
        }
    }
}
