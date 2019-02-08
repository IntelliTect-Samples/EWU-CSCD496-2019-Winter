﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using SecretSanta.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
	[ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

		public GroupController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET api/group
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
		[Produces(typeof(IEnumerable<GroupViewModel>))]
		public IActionResult GetAllGroups()
        {
            return Ok(Mapper.Map<IEnumerable<GroupViewModel>>(GroupService.FetchAll()));
		}

		// POST api/group
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		[Produces(typeof(GroupViewModel))]
		public IActionResult CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            return Ok(Mapper.Map<GroupViewModel>(GroupService.AddGroup(Mapper.Map<Group>(viewModel))));
        }

        // PUT api/group/5
        [HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
        [Produces(typeof(GroupViewModel))]
		public IActionResult UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
                return BadRequest();
            var fetchedGroup = GroupService.Find(id);
            if (fetchedGroup == null)
                return NotFound();

            fetchedGroup.Name = viewModel.Name;
            return Ok(Mapper.Map<GroupViewModel>(GroupService.UpdateGroup(fetchedGroup)));
        }

        [HttpPut("{groupId}/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
		public IActionResult AddUserToGroup(int groupId, int userId)
        {
            if (groupId <= 0)
                return BadRequest();
            if (userId <= 0)
                return BadRequest();
            if (GroupService.AddUserToGroup(groupId, userId))
                return Ok();
            return NotFound();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult DeleteGroup(int id)
        {
            if (id <= 0)
                return BadRequest("A group id must be specified");
            if (GroupService.DeleteGroup(id))
                return Ok();
            return NotFound();
        }
    }
}
