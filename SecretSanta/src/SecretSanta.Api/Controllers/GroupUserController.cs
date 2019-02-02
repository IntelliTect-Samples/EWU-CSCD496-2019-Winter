using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupUserController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new System.ArgumentNullException(nameof(groupService));
        }

        [HttpPut("{groupId}")]
        public ActionResult AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (_GroupService.AddUserToGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public ActionResult RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (_GroupService.RemoveUserFromGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
