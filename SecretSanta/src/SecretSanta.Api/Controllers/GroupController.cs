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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;
        
        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/Group/
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

        // PUT api/Group/
        [HttpPut]
        public ActionResult UpdateGroup(DTO.Group group)
        {
            if (group.Id <= 0) return NotFound();
            if (group is null) return BadRequest();
            Group databaseGroup = DTO.Group.ToEntity(group);
            _GroupService.UpdateGroup(databaseGroup);
            return Ok();
        }

        // DELETE api/Group/
        [HttpDelete]
        public ActionResult DeleteGroup(DTO.Group group)
        {
            if (group.Id <= 0) return NotFound();
            if (group is null) return BadRequest();
            Group databaseGroup = DTO.Group.ToEntity(group);
            _GroupService.DeleteGroup(databaseGroup);
            return Ok();
        }
    }
}
