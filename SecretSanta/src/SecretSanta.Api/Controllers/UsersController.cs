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
            var users = await UserService.FetchAll().ConfigureAwait(false);
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var fetchedUser = await UserService.GetById(id).ConfigureAwait(false);
            if (fetchedUser == null)
            {
                Log.Logger.Debug("id: " + id + " was invalid for GetUser(id)");
                return NotFound();
            }
            Log.Logger.Information("");
            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                Log.Logger.Debug("viewModel was null for CreateUser(viewModel)");
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel)).ConfigureAwait(false);
            Log.Logger.Information("");
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Debug("viewModel was null for UpdateUser(id, viewModel)");
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id).ConfigureAwait(false);
            if (fetchedUser == null)
            {
                Log.Logger.Debug("id: " + id + " was invalid for UpdateUser(id, viewModel)");
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser).ConfigureAwait(false);
            Log.Logger.Information("Successful call to UpdateUser(id, viewModel)");
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Debug("id: " + id + " was invalid for DeleteUser(id)");
                return BadRequest("A User id must be specified");
            }
            if (await UserService.DeleteUser(id).ConfigureAwait(false))
            {
                Log.Logger.Information("Successful call to DeleteUser(id)");
                return Ok();
            }
            Log.Logger.Debug("id: " + id + " was invalid for DeleteUser(id)");
            return NotFound();
        }
    }
}
