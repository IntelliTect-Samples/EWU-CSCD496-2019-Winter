using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO;
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

        // GET api/group
        [HttpGet]
        public ActionResult<IEnumerable<Group>> GetAllGroups()
        {
            return new ActionResult<IEnumerable<Group>>(_GroupService.FetchAll().Select(x => x.ToDTO()));
        }

        // POST api/group
        [HttpPost]
        public ActionResult<Group> CreateGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            return _GroupService.AddGroup(group.ToEntity()).ToDTO();
        }

        // PUT api/group/5
        [HttpPut]
        public ActionResult<Group> UpdateGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            return _GroupService.UpdateGroup(group.ToEntity()).ToDTO();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A group id must be specified");
            }

            if (_GroupService.DeleteGroup(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}