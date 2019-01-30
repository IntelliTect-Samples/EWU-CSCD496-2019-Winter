using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;
        
        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/Group/
        [HttpGet()]
        public ActionResult<List<DTO.Group>> GetAllGroups()
        {
            return _GroupService.GetAllGroups().Select(x => new DTO.Group(x)).ToList();
        }
    }
}
