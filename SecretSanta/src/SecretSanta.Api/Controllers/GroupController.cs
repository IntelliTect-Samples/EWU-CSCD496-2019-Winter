using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly HelperControllerMethods _HelperMethod = new HelperControllerMethods();

        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/Gift/5
        [HttpGet()]
        public ActionResult<List<DTO.Group>> GetListOfGroups()
        {
            List<Group> databaseGroups = _GroupService.GetAllGroups();
            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{dtoGroup}")]
        public ActionResult CreateGroup(DTO.Group dtoGroup)
        {
            if (_HelperMethod.IsNull(dtoGroup))
            {
                return BadRequest();
            }

            _GroupService.AddGroup(DTO.Group.ToEntity(dtoGroup));
            return Ok("Group added!");
        }

        [HttpDelete("{dtoGroup}")]
        public ActionResult DeleteGroup(DTO.Group dtoGroup)
        {
            if (_HelperMethod.IsNull(dtoGroup))
            {
                return BadRequest();
            }

            _GroupService.RemoveGroup(DTO.Group.ToEntity(dtoGroup));

            return Ok("Gift removed!");
        }

        [HttpPost("{dtoGroup}")]
        public ActionResult UpdateGroup(DTO.Group dtoGroup)//Update
        {
            if (_HelperMethod.IsNull(dtoGroup))
            {
                return BadRequest();
            }

            _GroupService.UpdateGroup(DTO.Group.ToEntity(dtoGroup));

            return Ok("Gift updated!");
        }

        [HttpPost("{dtoGroupId, dtoUser}")]
        public ActionResult<DTO.User> AddUserToGroup(int dtoGroupId, DTO.User dtoUser)
        {
            if (_HelperMethod.IsValidId(dtoGroupId))
            {
                return NotFound();
            }
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }

            return new DTO.User(_GroupService.AddUserToGroup(dtoGroupId, DTO.User.ToEntity(dtoUser)));
        }

        [HttpDelete("{dtoGroupId, dtoUser}")]
        public ActionResult<DTO.User> RemoveUserFromGroup(int dtoGroupId, DTO.User dtoUser)
        {
            if (dtoGroupId <= 0)
            {
                return NotFound();
            }
            if (dtoUser == null)
            {
                return BadRequest();
            }

            return new DTO.User(_GroupService.RemoveUserFromGroup(dtoGroupId, DTO.User.ToEntity(dtoUser)));
        }
    }
}
