using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // POST api/User/
        [HttpPost]
        public ActionResult<DTO.User> AddUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            return new DTO.User(_UserService.AddUser(DTO.User.ToDomain(user)));
        }

        // PUT api/User/
        [HttpPut]
        public ActionResult<DTO.User> UpdateUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            return new DTO.User(_UserService.UpdateUser(DTO.User.ToDomain(user)));
        }

        // DELETE api/User/
        [HttpDelete]
        public ActionResult DeleteUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.DeleteUser(DTO.User.ToDomain(user));
            return Ok();
        }
    }
}