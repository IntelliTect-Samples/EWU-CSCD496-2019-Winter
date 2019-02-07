using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;
using SecretSanta.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }
        public object UserModel { get; private set; }

        public UserController(IUserService userService, IMapper mapper)
        {
            Mapper = mapper;
            UserService = userService;
        }

        // GET api/user
        [HttpGet]
        [Produces(typeof(List<UserViewModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            return Ok(UserService.FetchAll().Select(x => Mapper.Map<UserViewModel>(x)));
        }

        // GET api/user/5
        [HttpGet("{id}")]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            User user = UserService.Find(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(user));
        }

        // POST api/<controller>
        [HttpPost]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }

            var persistedUser = UserService.AddUser(UserInputViewModel.ToModel(userViewModel));

            return Ok(Mapper.Map<UserViewModel>(persistedUser));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Produces(typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }

            var foundUser = UserService.Find(id);
            if (foundUser == null)
            {
                return NotFound();
            }

            foundUser.FirstName = userViewModel.FirstName;
            foundUser.LastName = userViewModel.LastName;

            var persistedUser = UserService.UpdateUser(foundUser);

            return Ok(Mapper.Map<UserViewModel>(persistedUser));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            bool userWasDeleted = UserService.DeleteUser(id);

            return userWasDeleted ? (IActionResult)Ok() : (IActionResult)NotFound();
        }
    }
}
