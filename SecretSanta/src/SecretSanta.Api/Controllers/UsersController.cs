using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
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
        [Produces(typeof(ICollection<UserViewModel>))]
        [ProducesResponseType(typeof(ICollection<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok((await UserService.FetchAll().ConfigureAwait(false)).Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            User fetchedUser = await UserService.GetById(id).ConfigureAwait(false);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                return BadRequest();
            }

            User createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel)).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            User fetchedUser = await UserService.GetById(id).ConfigureAwait(false);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            if (await UserService.DeleteUser(id).ConfigureAwait(false))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
