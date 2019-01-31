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

        // GET api/User/5
        [HttpGet("{id}")]
        public ActionResult<DTO.User> FindUser(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            User user = _UserService.Find(id);

            if (user != null)
            {
                return new DTO.User(user);
            }
            else
            {
                return BadRequest("Find error");
            }
        }

        // POST api/User/
        [HttpPost]
        public ActionResult MakeUser(string info)
        {
            if (info == null) return BadRequest();

            if (_UserService.MakeUser(info))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Make error");
            }
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
                return BadRequest("Bad data packet");
            }

            if (_UserService.Find(id) == null)
                return BadRequest("User not found");

            if (_UserService.UpsertUser(DTO.User.GetDomainUser(upDatedUser)))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Update Error");
            }
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (_UserService.DeleteUser(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Delete error");
            }
        }
    }
}
