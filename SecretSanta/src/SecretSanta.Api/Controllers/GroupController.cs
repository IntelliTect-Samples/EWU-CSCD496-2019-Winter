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

        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/Gift/5
        [HttpGet]
        public ActionResult<List<DTO.Group>> GetListOfGroups()
        {
            List<Group> databaseGroups = _GroupService.GetAllGroups();
            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPut("{dtoGroupId}")]
        public ActionResult CreateGroup(int dtoGroupId, DTO.Group dtoGroup)
        {
            if (dtoGroupId <= 0)
            {
                return BadRequest();
            }
            if (dtoGroup == null)
            {
                return NotFound();
            }

            _GroupService.AddGroup(dtoGroupId, DTO.Group.ToEntity(dtoGroup));
            return Ok("Group added!");
        }

        [HttpDelete("{dtoGroupId}")]
        public ActionResult DeleteGroup(int dtoGroupId, DTO.Group dtoGroup)
        {
            if (dtoGroup == null)
            {
                return BadRequest();
            }
            if (dtoGroupId <= 0)
            {
                return NotFound();
            }

            _GroupService.RemoveGroup(dtoGroupId, DTO.Group.ToEntity(dtoGroup));

            return Ok("Gift removed!");
        }

        [HttpPost("{dtoGroupId}")]
        public ActionResult UpdateGroup(int dtoGroupId, DTO.Group dtoGroup)//Update
        {
            if (dtoGroup == null)
            {
                return BadRequest();
            }
            if (dtoGroupId <= 0)
            {
                return NotFound();
            }

            _GroupService.UpdateGroup(dtoGroupId, DTO.Group.ToEntity(dtoGroup));

            return Ok("Gift updated!");
        }

        [HttpPut]
        public ActionResult<DTO.User> AddUserToGroup(int dtoGroupId, DTO.User dtoUser)
        {
            if (dtoGroupId <= 0)
            {
                return NotFound();
            }
            if (dtoUser == null)
            {
                return BadRequest();
            }

            return new DTO.User(_GroupService.AddUserToGroup(dtoGroupId, DTO.User.ToEntity(dtoUser)));
        }

        [HttpDelete]
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