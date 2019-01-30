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

        // GET api/User/
        [HttpGet("{groupId}")]
        public ActionResult<List<DTO.User>> GetUserForGroup(int groupId)
        {
            if (groupId <= 0) return NotFound();

            List<User> databaseUsers = _UserService.GetUsersForGroup(groupId);
            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }

        [HttpDelete()]
        public ActionResult<DTO.User> DeleteUser(int userId)
        {
            if (userId <= 0) return NotFound();
            return new DTO.User(_UserService.DeleteUser(userId));
        }

        // POST
        [HttpPost()]
        public ActionResult<DTO.User> CreateUser(DTO.User user)
        {
            if (user is null) return BadRequest();

            User databaseUser = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return new DTO.User(_UserService.CreateUser(databaseUser));
        }

        // PUT
        [HttpPut()]
        public ActionResult<DTO.User> UpdateUser(DTO.User user, int userId)
        {
            if (user is null) return BadRequest();

            SecretSanta.Domain.Models.User databaseUser = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return new DTO.User(_UserService.UpdateUser(databaseUser, userId));
        }
    }
}
