using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<List<DTO.User>> FetchAll()
        {
            return _UserService.FetchAll().Select(x => new DTO.User(x)).ToList();
        }

        [HttpPost]
        public ActionResult AddUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.AddUser(DTO.User.ToModelUser(user));
            return Ok("user successfully added");
        }

        [HttpPut]
        public ActionResult UpdateUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.UpdateUser(DTO.User.ToModelUser(user));
            return Ok("user successfully added");
        }

        [HttpDelete]
        public ActionResult RemoveUser(DTO.User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _UserService.RemoveUser(DTO.User.ToModelUser(user));
            return Ok("user successfully removed");

        }
    }
}