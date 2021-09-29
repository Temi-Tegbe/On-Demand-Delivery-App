

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

    public class CustomersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthorizationHelper _authHelper;

        public CustomersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, ICustomerRepository customerRepository, IAuthorizationHelper authHelper)


        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _customerRepository = customerRepository;
            _authHelper = authHelper;
        }

        [HttpPost]
        [Route("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistration model)

        {
            Response responseBody = new Response();

            ApplicationUser customerExist = await _userManager.FindByEmailAsync(model.Email);
            if (customerExist != null)

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

            await _customerRepository.AddAsync(model, user);

            if (!await _roleManager.RoleExistsAsync("customer"))
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "Customer" });

            if (await _roleManager.RoleExistsAsync("Customer"))
                await _userManager.AddToRoleAsync(user, "Customer");

            responseBody.Message = "Registration was successful.";
            responseBody.Status = "Success";
            responseBody.Payload = null;
            return Created($"/users/{user.Id}", responseBody);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Administrator")]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            Response responseBody = new Response();
            var customers = await _customerRepository.GetAllAsync();
            // Response body when fetched
            if (customers != null)
            {
                responseBody.Message = "Sucessfully fetched all customers";
                responseBody.Status = "Success";
                responseBody.Payload = customers;
                return Ok(responseBody);
            }

            // Set response body when not fetched
            responseBody.Message = "Customers fetch failed";
            responseBody.Status = "Failed";
            responseBody.Payload = null;
            return Ok(responseBody);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] long id)
        {
            Response responseBody = new Response();

            if (await _authHelper.CurrentUserHasRoleAsync("Administrator") == false && _authHelper.GetCurrentCustomerId() != id)
            {
                responseBody.Message = "Sorry, you are not permitted to view this candidate's profile.";
                responseBody.Payload = null;
                responseBody.Status = "Failed";
                return Forbid();
            }

            var customer = await _customerRepository.GetByIdAsync(id);

            // Reponse body when not found
            if (customer == null)
            {
                responseBody.Message = "Customer with corresponding id does not exists";
                responseBody.Status = "Failed";
                responseBody.Payload = null;
                return NotFound(responseBody);
            }

            // Set response body when found
            responseBody.Message = "Sucessfully fetched Customer with id";
            responseBody.Status = "Success";
            responseBody.Payload = customer;

            return Ok(responseBody);
        }

    }
}

