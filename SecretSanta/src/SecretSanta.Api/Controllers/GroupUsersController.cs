﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.Services.Interfaces;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class GroupUsersController : ControllerBase
    {
        private IGroupUserService GroupService { get; set; }
        private IMapper Mapper { get; set; }

        public GroupUsersController(IGroupUserService groupService, IMapper mapper)
        {
            GroupService = groupService;
            Mapper = mapper;
        }

        [HttpPut("{groupId}")]
        public async Task<IActionResult> AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            if (userId <= 0)
            {
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
