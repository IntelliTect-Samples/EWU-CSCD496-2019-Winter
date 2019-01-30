using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult MakeUser(string info)
        {
            if (info == null) return BadRequest();

            _UserService.MakeUser(info);

            return Ok();
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, DTO.User upDatedUser)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            if(upDatedUser == null)
            {
                return BadRequest();
            }

            User user = new User()
            {
                Id = id,
                First = upDatedUser.First,
                Last = upDatedUser.Last,
                Gifts = upDatedUser.Gifts,
                UserGroups = upDatedUser.UserGroups
            };

            _UserService.UpsertUser(user);

            return Ok();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            _UserService.DeleteUser(id);

            return Ok();
        }
    }
}
