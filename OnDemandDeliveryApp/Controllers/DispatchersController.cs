

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnDemandDeliveryApp.Application.Helpers;
using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DispatchersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IDispatcherRepository _dispatcherRepository;
        private readonly IAuthorizationHelper _authHelper;

        public DispatchersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, IDispatcherRepository dispatcherRepository, IAuthorizationHelper authHelper)


        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dispatcherRepository = dispatcherRepository;
            _authHelper = authHelper;
        }

        [HttpPost]
        [Route("register-dispatcher")]
        public async Task<IActionResult> RegisterDispatcher([FromBody] DispatcherRegistration model)

        {
            Response responseBody = new Response();

            ApplicationUser dispatcherExist = await _userManager.FindByEmailAsync(model.Email);
            if (dispatcherExist != null)

            {
                responseBody.Message = "A user with this email address already exists";
                responseBody.Status = "Failed";
                responseBody.Payload = null;
                return Conflict(responseBody);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };


            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                responseBody.Message = "Registration was not successful. Please try again";
                responseBody.Status = "Failed";
                responseBody.Payload = result;
                return BadRequest(responseBody);
            }

            await _dispatcherRepository.AddAsync(model, user);

            if (!await _roleManager.RoleExistsAsync("dispatcher"))
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "Dispatcher" });

            if (await _roleManager.RoleExistsAsync("Dispatcher"))
                await _userManager.AddToRoleAsync(user, "Dispatcher");

            responseBody.Message = "Registration was successful.";
            responseBody.Status = "Success";
            responseBody.Payload = null;
            return Created($"/users/{user.Id}", responseBody);
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Administrator")]
        public async Task<ActionResult<List<Dispatcher>>> GetAllDispatchers()
        {
            Response responseBody = new Response();
            var dispatchers = await _dispatcherRepository.GetAllAsync();
            // Response body when fetched
            if (dispatchers != null)
            {
                responseBody.Message = "Sucessfully fetched all dispatchers";
                responseBody.Status = "Success";
                responseBody.Payload = dispatchers;
                return Ok(responseBody);
            }

            // Set response body when not fetched
            responseBody.Message = "Dispatchers fetch failed";
            responseBody.Status = "Failed";
            responseBody.Payload = null;
            return Ok(responseBody);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Dispatcher>> GetDispatcherAsync([FromRoute] long id)
        {
            Response responseBody = new Response();

            if (await _authHelper.CurrentUserHasRoleAsync("Administrator") == false && _authHelper.GetCurrentCustomerId() != id)
            {
                responseBody.Message = "Sorry, you are not permitted to view this dispatcher's profile.";
                responseBody.Payload = null;
                responseBody.Status = "Failed";
                return Forbid();
            }

            var dispatcher = await _dispatcherRepository.GetByIdAsync(id);

            // Reponse body when not found
            if (dispatcher == null)
            {
                responseBody.Message = "Dispatcher with corresponding id does not exists";
                responseBody.Status = "Failed";
                responseBody.Payload = null;
                return NotFound(responseBody);
            }

            // Set response body when found
            responseBody.Message = "Sucessfully fetched Dispatcher with id";
            responseBody.Status = "Success";
            responseBody.Payload = dispatcher;

            return Ok(responseBody);
        }
    }
}

