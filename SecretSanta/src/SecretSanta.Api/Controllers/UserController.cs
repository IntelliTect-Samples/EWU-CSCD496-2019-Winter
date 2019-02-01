using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.DTO
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        // Create/Update/Delete users
        // Create/Update/Delete gift for a user


        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // GET api/Gift/5
        [HttpPost]
        public void CreateUser(User user)
        {
            
        }

        [HttpPut("{id}")]
        public void 

        [HttpDelete("{id}")]

    }
}
