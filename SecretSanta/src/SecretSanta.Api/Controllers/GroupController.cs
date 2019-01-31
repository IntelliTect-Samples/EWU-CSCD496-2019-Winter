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

        private Group GroupDtoToEntity(DTO.Group dtoGroup)
        {
            List<GroupUser> copyUserGroup = new List<GroupUser>();
            copyUserGroup.AddRange(dtoGroup.GroupUsers.Select(groupUser =>
                        new GroupUser
                        {
                            Group = groupUser.Group,
                            GroupId = groupUser.GroupId,
                            User = groupUser.User,
                            UserId = groupUser.UserId
                        }));


            Group entity = new Group//same arguements as constructor
            {
                Id = dtoGroup.Id,
                Name = dtoGroup.Name,
                GroupUsers = copyUserGroup
            };

            return entity;
        }

        // GET api/Gift/5
        [HttpGet()]
        public ActionResult<List<DTO.Group>> GetUsersInGroup()
        {
            List<Group> databaseGroups = _GroupService.FetchAll();
            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{dtoGroup}")]
        public ActionResult AddGroup(DTO.Group dtoGroup)
        {
            if (dtoGroup == null)
            {
                return BadRequest();
            }

            _GroupService.AddGroup(DTO.Group.ToEntity(dtoGroup));
            return Ok("Group added!");

            //return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpDelete("{dtoGroup}")]
        public ActionResult DeleteGroup(DTO.Group dtoGroup)
        {
            if (dtoGroup == null)
            {
                return BadRequest();
            }
            _GroupService.RemoveGroup(DTO.Group.ToEntity(dtoGroup));

            return Ok("Gift removed!");
        }

        [HttpPost("{dtoGroup}")]
        public ActionResult UpdateGroup(DTO.Group dtoGroup)//Update
        {
            if (dtoGroup == null)
            {
                return BadRequest();
            }

            Domain.Models.Group originalGroup = DTO.Group.ToEntity(dtoGroup);
            Domain.Models.Group updateGroup = _GroupService.UpdateGroup(DTO.Group.ToEntity(dtoGroup));

            //?Check if gift is updated here or in test?

            return Ok("Gift updated!");
        }
    }
}
