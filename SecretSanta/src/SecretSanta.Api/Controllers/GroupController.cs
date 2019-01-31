using System;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        // Create group
        // AddGroup

        // Update group
        // UpdateGroup

        // Delete group
        // TODO:

        // Add user to Group
        // TODO: 

        // Remove user from group
        // TODO: 

        // Get all groups
        // FetchAll

        // Get users from groups
        // TODO:
    }
}