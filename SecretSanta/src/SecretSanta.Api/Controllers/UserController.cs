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
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        public IMapper Mapper { get; }

        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpGet("id")]
        [Produces(typeof(ICollection<UserViewModel>))]
        public IActionResult/*<UserViewModel>*/ Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"passed in id {id} must be greater than 0");
            }
            var foundUser = UserService.Find(id);
            if (foundUser == null)
            {
                return NotFound($"passed in id {id} could not match a user");
            }
            else
            {
                //return Ok(UserViewModel.ToViewModel(foundUser));
                var mapResult = Mapper.Map<UserViewModel>(foundUser);

                return Ok(mapResult);
            }
        }

        // POST api/<controller>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPost]
        [Produces(typeof(ICollection<UserViewModel>))]
        public IActionResult/*<UserViewModel>*/ Post(UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest("passed in userInputViewModel cannot be null");
            }
            else
            {
                var persistedUser = UserService.AddUser(Mapper.Map<User>(userViewModel));

                return CreatedAtAction(nameof(Get), new { id = persistedUser.Id }, Mapper.Map<UserViewModel>(persistedUser));
            }
            //return Ok(UserViewModel.ToViewModel(persistedUser));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(ICollection<User>))]
        public IActionResult/*<UserViewModel>*/ Put(int id, UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest($"passed in userViewModel may not be null");
            }

            var foundUser = UserService.Find(id);
            if (foundUser == null)
            {
                return NotFound($"user not found from given id of {id}");
            }

            /*
            foundUser.FirstName = userViewModel.FirstName;
            foundUser.LastName = userViewModel.LastName;
            */
            else
            {
                Mapper.Map(userViewModel, foundUser);
                var persistedUser = UserService.UpdateUser(foundUser);

                return Ok(persistedUser);
            }
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"passed in id {id} must be greater than 0");
            }
            else
            {
                try
                {
                    UserService.DeleteUser(id);
                    return Ok($"user {id} was deleted");
                }
                catch (InvalidOperationException e)
                {
                    return NotFound($"user {id} was not found, therefore not deleted");
                }
            }
        }
    }
}
