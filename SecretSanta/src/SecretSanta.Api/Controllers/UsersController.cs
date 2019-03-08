using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }

        public UsersController(IUserService userService, IMapper mapper)
        {
            Log.Logger.Debug("Properties inilized via constructor paramaters: GiftService = {userService}, Mapper = {mapper}", userService, mapper);
            UserService = userService;
            Mapper = mapper;
        }

        // GET api/User
        [HttpGet]
        public async Task<ActionResult<ICollection<UserViewModel>>> GetAllUsers()
        {
            var users = await UserService.FetchAll().ConfigureAwait(false);
            Log.Logger.Debug("Fetched users obtained using FetchAll method. Returning selected users inside a OK Request Reponse.");
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            StatusCodeResult statusCodeResult = null;

            Log.Logger.Information("Attempting to fetch users using passed in Id. Id = {id}", id);
            var fetchedUser = await UserService.GetById(id).ConfigureAwait(false);

            if (fetchedUser == null)
            {
                Log.Logger.Warning("Fetched Users was null. Unable to obtain users using passed in Id. Id = {id}", id);
                statusCodeResult = NotFound();
            }
            else
            {
                Log.Logger.Debug("Fetched users were not null. Mapping Fetched Users.");
                Mapper.Map<UserViewModel>(fetchedUser);
                statusCodeResult = Ok();
            }

            Log.Logger.Debug("Returned statusCode {nameof(statusCodeResult)}", nameof(statusCodeResult));
            return statusCodeResult;
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            if (User == null)
            {
                Log.Logger.Warning("User was null when attempting to createUser");
                Log.Logger.Warning("Returned Request Response: {nameof(BadRequest()}", BadRequest());
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel)).ConfigureAwait(false);
            Log.Logger.Debug("Valid User added using passed in view Model. Request Response: CreatedAtAction");
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            StatusCodeResult statusCodeResult = null;

            if (viewModel == null)
            {
                Log.Logger.Warning("Passed in viewModel is null");
                statusCodeResult = BadRequest();
            }

            else
            {
                Log.Logger.Information("Attempting to get User by Id. Id = {userId}", id);
                var fetchedUser = await UserService.GetById(id).ConfigureAwait(false);

                if (fetchedUser == null)
                {
                    Log.Logger.Warning("No users were fetched when attampting to fetch users using ID. ID = {id}", id);
                    statusCodeResult = NotFound();
                }
                else
                {
                    Log.Logger.Information("Valid userId and viewModel being used to update User. UserId: {id}, ViewModel: {nameof(viewModel}", id, nameof(viewModel));
                    Mapper.Map(viewModel, fetchedUser);
                    await UserService.UpdateUser(fetchedUser).ConfigureAwait(false);
                    statusCodeResult = NoContent();
                }
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            StatusCodeResult statusCodeResult = null;

            if (id <= 0)
            {
                Log.Logger.Warning("Invalid user ID passed in to search for User to Delete. Id must be greater than 0. User ID = {id}", id);
                statusCodeResult = BadRequest();
            }

            else if (await UserService.DeleteUser(id).ConfigureAwait(false))
            {
                Log.Logger.Debug("Valid user ID passed in to search for User to Delete. User with passed in Id was deleted. User ID = {id}", id);
                statusCodeResult = Ok();
            }
            else
            {
                Log.Logger.Warning("Valid user ID passed in, but user was not found. USER ID = {id}", id);
                statusCodeResult = NotFound();
            }

            Log.Logger.Debug($"Returned statusCode {nameof(statusCodeResult)}");
            return statusCodeResult;
        }
    }
}
