using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public ActionResult AddGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }
            _GroupService.AddGroup(DTO.Group.ToModelGroup(group));
            return Ok("group successfully added");
        }

        [HttpPut]
        public ActionResult UpdateGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }
            _GroupService.UpdateGroup(DTO.Group.ToModelGroup(group));
            return Ok("group successfully updated");
        }
        [HttpDelete]
        public ActionResult RemoveGroup(DTO.Group group)
        {
            if (group == null)
            {
                return BadRequest();
            }
            _GroupService.RemoveGroup(DTO.Group.ToModelGroup(group));
            return Ok("group successfully removed");
        }

        [HttpGet ("/getUsers")]
        public ActionResult<List<DTO.User>> FetchGroupUsers(int groupId)
        {
            return _GroupService.FetchGroupUsers(groupId).Select(x => new DTO.User(x)).ToList();
        }

        [HttpGet]
        public ActionResult<List<DTO.Group>> FetchAll()
        {
            return _GroupService.FetchAll().Select(x => new DTO.Group(x)).ToList();
        }

    }
}