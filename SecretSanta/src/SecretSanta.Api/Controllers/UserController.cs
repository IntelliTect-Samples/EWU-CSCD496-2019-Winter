using System;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET api/Gift/5
        [HttpPost] // Create
        public ActionResult<DTO.User> AddUser([FromBody] DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var addedUser = _userService.AddUser(DTO.User.ToEntity(user));
            return Ok(new DTO.User(addedUser));
        }

        // PUT api/User
        [HttpPut] // Update
        public ActionResult<DTO.User> UpdateUser([FromBody] DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var updatedUser = _userService.UpdateUser(DTO.User.ToEntity(user));
            return Ok(new DTO.User(updatedUser));
        }

        // DELETE api/User    
        [HttpDelete] // Delete
        public ActionResult DeleteUser([FromBody] DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userService.DeleteUser(DTO.User.ToEntity(user));

            return Ok();
        }
    }
}