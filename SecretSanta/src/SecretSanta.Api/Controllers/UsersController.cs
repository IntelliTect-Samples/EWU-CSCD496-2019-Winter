using System;
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
        public async Task<IActionResult> Get()
        {
            List<User> users = await UserService.FetchAll();
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> Get(int id)
        {
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> Post(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel));

            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, UserInputViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            if (await UserService.DeleteUser(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
