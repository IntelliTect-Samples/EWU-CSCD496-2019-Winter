using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Http;

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
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                Log.Logger.Debug($"{nameof(fetchedUser)} is null after function call {nameof(UserService.GetById)}. Not Found.");
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                Log.Logger.Debug($"{nameof(User)} is null before function call {nameof(UserService.AddUser)}. Bad Request.");
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel));

            Log.Logger.Information($"User created.", viewModel);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Debug($"{nameof(viewModel)} is null before function call {nameof(UserService.GetById)}. Bad Request.");
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                Log.Logger.Debug($"{nameof(fetchedUser)} is null after function call {nameof(UserService.GetById)}. Not Found.");
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser);
            Log.Logger.Information($"User updated.", viewModel);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Debug($"{nameof(id)} is not valid before function call {nameof(UserService.DeleteUser)}. Bad Request.");
                return BadRequest("A User id must be specified");
            }

            if (await UserService.DeleteUser(id))
            {
                Log.Logger.Information($"User deleted.", id);
                return Ok();
            }
            Log.Logger.Debug($"{nameof(id)} is not valid after function call {nameof(UserService.DeleteUser)}. Not Found.");
            return NotFound();
        }
    }
}
