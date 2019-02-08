using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
            Mapper.Map(userViewModel, foundUser);


            var persistedUser = UserService.UpdateUser(foundUser);

            return Ok(Mapper.Map<UserViewModel>(persistedUser));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            bool userWasDeleted = UserService.DeleteUser(id);

            return userWasDeleted ? (ActionResult)Ok() : (ActionResult)NotFound();
        }
    }
}
