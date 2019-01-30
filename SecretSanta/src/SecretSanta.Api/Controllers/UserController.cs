using System;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO;
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

        // GET api/Gift/5
        [HttpGet] // Create
        public ActionResult<User> AddUser(User user)
        {
            if (user == null) return BadRequest();

            var addedUser = _UserService.AddUser(DTO.User.ToEntity(user));
            return Ok(new User(addedUser));
        }

        // PUT api/User
        [HttpPut] // Update
        public ActionResult<User> UpdateUser(User user)
        {
            if (user == null) return BadRequest();

            var updatedUser = _UserService.UpdateUser(DTO.User.ToEntity(user));
            return Ok(new User(updatedUser));
        }

        // DELETE api/User    
        [HttpDelete] // Delete
        public ActionResult DeleteUser(User user)
        {
            if (user == null) return BadRequest();

            _UserService.DeleteUser(DTO.User.ToEntity(user));

            return Ok();
        }
    }
}