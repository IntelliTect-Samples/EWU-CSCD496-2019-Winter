using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            Log.Logger.Debug($"Properties inilized via constructor paramaters: GroupService = {nameof(groupService)}, Mapper = {nameof(mapper)}");
            GroupService = groupService;
            Mapper = mapper;
        }

        // GET api/group
        [HttpGet]
        public async Task<ActionResult<ICollection<GroupViewModel>>> GetGroups()
        {
            var groups = await GroupService.FetchAll().ConfigureAwait(false);
            Log.Logger.Debug("Group(s) found and returned using GetGroups");
            Log.Logger.Debug($"Returned statusCode Ok()");
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            Log.Logger.Information($"Attempting to getById using passed in GroupId for GetGroup. Group Id = {id}");
            var group = await GroupService.GetById(id).ConfigureAwait(false);

            if (group == null)
            {
                Log.Logger.Warning($"Invalid group Id passed into GetGroup, Id must be greater than 0. Group Id = {id}");
                Log.Logger.Debug($"Returned statusCode BadRequest()");
                return NotFound();
            }

            Log.Logger.Debug($"Valid group Id passed into GetGroup, Group found and returned. Group Id = {id}");
            Log.Logger.Debug($"Returned statusCode Ok()");
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning("Invalid viewModel passed into UpdateGroup. viewModel cannot be null");
                Log.Logger.Debug($"Returned statusCode BadRequest()");
                return BadRequest();
            }
            else
            {
                Log.Logger.Debug("Group Created using passed in viewModel");

                var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel)).ConfigureAwait(false);

                Log.Logger.Debug($"Created group. Group Id = {createdGroup.Id}, Group = {nameof(GetGroup)}");
                Log.Logger.Debug($"Returned statusCode CreatedAtAction");
                return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id }, Mapper.Map<GroupViewModel>(createdGroup));
            }
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            StatusCodeResult statusCodeResult = null;

            if (viewModel == null)
            {
                Log.Logger.Warning("Invalid viewModel passed into UpdateGroup. viewModel cannot be null");
                statusCodeResult = BadRequest();
            }
            else
            {
                var group = await GroupService.GetById(id).ConfigureAwait(false);
                if (group == null)
                {
                    Log.Logger.Warning("Invalid group found. No found group, resulting in GetByID returning null group");
                    statusCodeResult = NotFound();
                }
                else
                {
                    Log.Logger.Debug($"Valid Group found. Updated group using group Id. Group Id = {id}");

                    Mapper.Map(viewModel, group);
                    await GroupService.UpdateGroup(group).ConfigureAwait(false);

                    statusCodeResult = NoContent();
                }
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            StatusCodeResult statusCodeResult = null;
            if (id <= 0)
            {
                Log.Logger.Warning($"Invalid group ID passed in to search for Group. Id must be greater than 0. Group ID = {id}");
                statusCodeResult = BadRequest();
            }

            else if (await GroupService.DeleteGroup(id).ConfigureAwait(false))
            {
                Log.Logger.Debug($"Valid groupId. Removed group. Group Id = {id}");
                statusCodeResult = Ok();
            }
            else
            {
                Log.Logger.Warning("Group was not found, group was not removed");
                statusCodeResult = NotFound();
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }
    }
}
