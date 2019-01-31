using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly HelperControllerMethods _HelperMethod = new HelperControllerMethods();

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet()]
        public ActionResult<List<DTO.User>> GetUsersInGroup()
        {
            List<User> databaseUsers = _UserService.FetchAll();
            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{dtoUserId}")]
        public ActionResult AddUser(int dtoUserId, DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }
            if (_HelperMethod.IsValidId(dtoUserId))
            {
                return NotFound();
            }

            _UserService.AddUser(dtoUserId, DTO.User.ToEntity(dtoUser));
            return Ok("User added!");
        }

        [HttpDelete("{dtoUserId}")]
        public ActionResult RemoveUser(int dtoUserId, DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }
            if (_HelperMethod.IsValidId(dtoUserId))
            {
                return NotFound();
            }

            _UserService.RemoveUser(dtoUserId, DTO.User.ToEntity(dtoUser));

            return Ok("User removed!");
        }

        [HttpPost("{userId}")]
        public ActionResult UpdateUser(int userId, DTO.User dtoUser)//Update
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }
            if (_HelperMethod.IsValidId(userId))
            {
                return NotFound();
            }

            _UserService.UpdateUser(userId, DTO.User.ToEntity(dtoUser));

            return Ok("Gift updated!");
        }


    }
}
