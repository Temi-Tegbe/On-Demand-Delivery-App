using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public class AdministratorsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAdministratorRepository _administratorRepository;

        public AdministratorsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
                IConfiguration configuration, IAdministratorRepository administratorRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _administratorRepository = administratorRepository;

        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdministratorRegistration model)
        {
            Response responseBody = new Response();


            ApplicationUser administratorExist = await _userManager.FindByEmailAsync(model.Email);
            if (administratorExist != null)

            {
                responseBody.Message = "An Administrator with this email already exists.";
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
                responseBody.Message = "Administrator registration was not successful. Please try again.";
                responseBody.Status = "Failed";
                responseBody.Payload = null;
                return BadRequest(responseBody);
            }


            await _administratorRepository.AddAsync(model, user);

            if (!await _roleManager.RoleExistsAsync("Administrator"))
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "Administrator" });

            if (await _roleManager.RoleExistsAsync("Administrator"))
                await _userManager.AddToRoleAsync(user, "Administrator");

            responseBody.Message = "Administrator registration completed successfully.";
            responseBody.Status = "Success";
            responseBody.Payload = null;
            return Created($"/users/{user.Id}", responseBody);

        }
    }
}
