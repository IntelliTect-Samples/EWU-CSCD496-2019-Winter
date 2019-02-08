﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get; }

        public GroupController(IGroupService groupService)
        {
            GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // GET api/group
        [HttpGet]
        public ActionResult<IEnumerable<GroupViewModel>> GetAllGroups()
        {
            return new ActionResult<IEnumerable<GroupViewModel>>(GroupService.FetchAll().Select(x => GroupViewModel.ToViewModel(x)));
        }

        // POST api/group
        [HttpPost]
        public ActionResult<GroupViewModel> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            return GroupViewModel.ToViewModel(GroupService.AddGroup(GroupInputViewModel.ToModel(viewModel)));
        }

        // PUT api/group/5
        [HttpPut("{id}")]
        public ActionResult<GroupViewModel> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedGroup = GroupService.Find(id);
            if (fetchedGroup == null)
            {
                return NotFound();
            }

            fetchedGroup.Name = viewModel.Name;

            return GroupViewModel.ToViewModel(GroupService.UpdateGroup(fetchedGroup));
        }

        [HttpPut("{groupId}/{userid}")]
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

            if (GroupService.AddUserToGroup(groupId, userId))
            {
                return Ok();
            }
            return NotFound();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(int id)
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
