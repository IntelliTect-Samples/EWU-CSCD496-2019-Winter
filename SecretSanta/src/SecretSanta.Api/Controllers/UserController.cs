using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("{userId}")]
        public ActionResult<DTO.User> GetUserById(int userId)
        {
            if (userId <= 0) return NotFound();
            
            return new DTO.User(_UserService.Find(userId));
        }

        [HttpGet("GroupId/{groupId}")]
        public ActionResult<List<DTO.User>> GetUserForGroup(int groupId)
        {
            if (groupId <= 0) return NotFound();

            List<User> databaseUsers = _UserService.GetUsersForGroup(groupId);
            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }

        [HttpDelete]
        public ActionResult DeleteUser(DTO.User user)
        {
            if (user is null) return BadRequest();
            if (user.Id <= 0) return NotFound();

            User databaseUser = DTO.User.ToEntity(user);
            if (_UserService.DeleteUser(databaseUser) is null) return NotFound();
            return Ok();
        }

        [HttpPost]
        public ActionResult CreateUser(DTO.User user)
        {
            if (user is null || user.Id > 0) return BadRequest();

            User databaseUser = DTO.User.ToEntity(user);

            _UserService.CreateUser(databaseUser);

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateUser(DTO.User user)
        {
            if (user is null) return BadRequest();

            SecretSanta.Domain.Models.User databaseUser = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            if (_UserService.UpdateUser(databaseUser, user.Id) is null) return NotFound();

            return Ok();
        }
    }
}
