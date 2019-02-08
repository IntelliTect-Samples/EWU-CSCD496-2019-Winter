﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<UserViewModel> Post(UserInputViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }

            var persistedUser = UserService.AddUser(UserInputViewModel.ToModel(userViewModel));

            return Ok(UserViewModel.ToViewModel(persistedUser));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult<UserViewModel> Put(int id, UserInputViewModel userViewModel)
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

            return Ok(UserViewModel.ToViewModel(persistedUser));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool userWasDeleted = UserService.DeleteUser(id);

            return userWasDeleted ? (ActionResult)Ok() : (ActionResult)NotFound();
        }
    }
}
