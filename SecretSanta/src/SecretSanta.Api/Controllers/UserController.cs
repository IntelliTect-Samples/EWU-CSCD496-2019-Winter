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

        private User UserDtoToEntity(DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                throw new ArgumentNullException(nameof(dtoUser));
            }

            List<Domain.Models.Gift> copyGifts = new List<Domain.Models.Gift>();
            copyGifts.AddRange(dtoUser.Gifts.Select(gift =>
            new Domain.Models.Gift
            {
                Id = gift.Id,
                Description = gift.Description,
                Title = gift.Title,
                OrderOfImportance = gift.OrderOfImportance,
                Url = gift.Url,
                User = gift.User,
                UserId = gift.UserId
            }));

            List<Domain.Models.GroupUser> copyGroup = new List<Domain.Models.GroupUser>();
            copyGroup.AddRange(dtoUser.GroupUsers.Select(groupUser =>
            new Domain.Models.GroupUser
            {
                Group = groupUser.Group,
                GroupId = groupUser.GroupId,
                User = groupUser.User,
                UserId = groupUser.UserId
            }));

            User entity = new User
            {
                Id = dtoUser.Id,
                FirstName = dtoUser.FirstName,
                LastName = dtoUser.LastName,
                Gifts = copyGifts,
                GroupUsers = copyGroup
            };

            return entity;
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

            _UserService.AddUser(UserDtoToEntity(dtoUser));
            return Ok("User added!");

            //return databaseUsers.Select(x => new DTO.Gift(x)).ToList();
        }

        [HttpDelete("{dtoUser}")]
        public ActionResult RemoveUser(DTO.User dtoUser)
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }
            _UserService.RemoveUser(UserDtoToEntity(dtoUser));

            return Ok("User removed!");
        }

        [HttpPost("{dtoUser}")]
        public ActionResult UpdateUser(DTO.User dtoUser)//Update
        {
            if (_HelperMethod.IsNull(dtoUser))
            {
                return BadRequest();
            }

            _UserService.UpdateUser(UserDtoToEntity(dtoUser));

            return Ok("Gift updated!");
        }
    }
}
