using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserController(IUserService UserService)
        {
            _UserService = UserService ?? throw new ArgumentNullException(nameof(UserService));
        }

        // GET api/User
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return new ActionResult<IEnumerable<User>>(_UserService.FetchAll().Select(x => x.ToDTO()));
        }

        // POST api/User
        [HttpPost]
        public ActionResult<User> CreateUser(User User)
        {
            if (User == null)
            {
                return BadRequest();
            }

            return _UserService.AddUser(User.ToEntity()).ToDTO();
        }

        // PUT api/User/5
        [HttpPut]
        public ActionResult<User> UpdateUser(User User)
        {
            if (User == null)
            {
                return BadRequest();
            }

            return _UserService.UpdateUser(User.ToEntity()).ToDTO();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A User id must be specified");
            }

            if (_UserService.DeleteUser(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}