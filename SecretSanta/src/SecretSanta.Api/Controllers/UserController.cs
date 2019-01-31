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

        // GET api/Gift/5
        [HttpGet()]
        public ActionResult<List<DTO.User>> GetUsersInGroup()
        {
            List<User> databaseUsers = _UserService.FetchAll();
            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }

        //POST api/Gift/4
        [HttpPost("{dtoUser}")]
        public ActionResult AddUser(DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }

            _UserService.AddUser(DTO.User.ToEntity(dtoUser));
            return Ok("User added!");
        }

        [HttpDelete("{dtoUser}")]
        public ActionResult RemoveUser(DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }
            _UserService.RemoveUser(DTO.User.ToEntity(dtoUser));

            return Ok("User removed!");
        }

        [HttpPost("{dtoUser}")]
        public ActionResult UpdateUser(DTO.User dtoUser)//Update
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }

            _UserService.UpdateUser(DTO.User.ToEntity(dtoUser));

            return Ok("Gift updated!");
        }


    }
}
