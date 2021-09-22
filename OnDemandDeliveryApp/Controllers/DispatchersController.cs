

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public DispatchersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, IDispatcherRepository dispatcherRepository)


        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dispatcherRepository = dispatcherRepository;
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


    }
}

