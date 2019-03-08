using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

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
                Log.Logger.Warning($"{nameof(fetchedUser)} was null in call to {nameof(UserService.GetById)}");
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} was null in call to {nameof(UserService.AddUser)}");
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel)).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                Log.Logger.Warning($"{nameof(viewModel)} was passed in as null in call to {nameof(UsersController.UpdateUser)}");
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id).ConfigureAwait(false);
            if (fetchedUser == null)
            {
                Log.Logger.Warning($"{nameof(fetchedUser)} was null after call to {nameof(UserService.GetById)}");
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                Log.Logger.Warning($"{nameof(id)} was invalid in call to {nameof(UserService.DeleteUser)}");
                return BadRequest("A User id must be specified");
            }

            if (await UserService.DeleteUser(id).ConfigureAwait(false))
            {
                Log.Logger.Verbose($"{nameof(id)} was passed in to {nameof(UserService.DeleteUser)} to successfully delete by id");
                return Ok();
            }
            return NotFound();
        }
    }
}
