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
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public UsersController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        // GET api/User
        [HttpGet]
        public async Task<ActionResult<ICollection<UserViewModel>>> GetAllUsers()
        {
            var users = await UserService.FetchAll();
            Log.Logger.Information($"All users fetched successfully. Status: 200");
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                Log.Logger.Warning($"User not found in GetUser. Status: 404");
                return NotFound();
            }

            Log.Logger.Information($"User successfully found. Status: 200");
            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                Log.Logger.Warning($"Argument: {nameof(User)} was null in CreateUser. Status: 400");
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel));

            Log.Logger.Information($"User successfully added. Status: 201");
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"Argument: {nameof(viewModel)} was null in UpdateUser. Status: 400");
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                Log.Logger.Warning($"User not found in UpdateUser. Status: 404");
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser);
            Log.Logger.Information($"User successfully updated. Status: 204");
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Warning($"Id<0 in DeleteUser. Status: 400");
                return BadRequest("A User id must be specified");
            }

            if (await UserService.DeleteUser(id))
            {
                Log.Logger.Information($"User successflly deleted. Status: 200");
                return Ok();
            }
            Log.Logger.Warning($"User not found in DeleteUser. Status: 404");
            return NotFound();
        }
    }
}
