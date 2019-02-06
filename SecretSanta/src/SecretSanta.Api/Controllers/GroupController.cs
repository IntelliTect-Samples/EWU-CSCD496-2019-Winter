﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService;
            Mapper = mapper;
        }

        // GET api/group
        [HttpGet]
        [Produces(typeof(ICollection<GroupViewModel>))]
        public IActionResult Get()
        {
            return Ok(GroupService.FetchAll().Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(GroupViewModel))]
        public IActionResult Get(int id)
        {
            var group = GroupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        [Produces(typeof(GroupViewModel))]
        public IActionResult Post(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var createdGroup = GroupService.AddGroup(Mapper.Map<Group>(viewModel));
            return CreatedAtAction(nameof(Get), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public IActionResult Put(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var group = GroupService.GetById(id);
            if (group == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            GroupService.UpdateGroup(group);

            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A group id must be specified");
            }

            if (GroupService.DeleteGroup(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
