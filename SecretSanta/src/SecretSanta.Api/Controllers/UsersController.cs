using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get()
        {
            List<User> fetchedUsers = await UserService.FetchAll();
            fetchedUsers.Select(x => Mapper.Map<UserViewModel>(x));

            List<User> fetchedUsersAgain = await UserService.FetchAll();
            return Ok(fetchedUsersAgain.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(ICollection<UserViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(int id)
        {
            User fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Mapper.Map<UserViewModel>(fetchedUser));
            }
        }

        // POST api/User
        [HttpPost]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                return BadRequest();
            }
            else
            {
                User createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel));

                return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
            }
        }

        // PUT api/User/5
        [HttpPut]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            else
            {
                User fetchedUser = await UserService.GetById(id);
                if (fetchedUser == null)
                {
                    return NotFound();
                }
                else
                {
                    Mapper.Map(viewModel, fetchedUser);

                    User updatedUser = await UserService.UpdateUser(fetchedUser);
                    return NoContent();
                }
            }
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }
            else
            {
                bool isDeleted = await UserService.DeleteUser(id);

                if (isDeleted)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
