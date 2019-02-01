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
        private IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public ActionResult<List<DTO.User>> GetUsersInGroup()
        {
            List<User> databaseUsers = _UserService.FetchAll();
            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{dtoUserId}")]
        public ActionResult AddUser(int dtoUserId, DTO.User dtoUser)
        {
            if (dtoUser == null)
            {
                return BadRequest();
            }
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            return Ok("User added!");
        }

        [HttpDelete]
        public ActionResult RemoveUser(int dtoUserId, DTO.User dtoUser)
        {
            if (dtoUser == null)
            {
                return BadRequest();
            }
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            _UserService.RemoveUser(dtoUserId, DTO.User.ToEntity(dtoUser));

            return Ok("User removed!");
        }

        [HttpPost]
        public ActionResult UpdateUser(int dtoUserId, DTO.User dtoUser)//Update
        {
            if (dtoUser == null)
            {
                return BadRequest();
            }
            if (dtoUserId <= 0)
            {
                return NotFound();
            }

            _UserService.UpdateUser(dtoUserId, DTO.User.ToEntity(dtoUser));

            return Ok("Gift updated!");
        }


    }
}
