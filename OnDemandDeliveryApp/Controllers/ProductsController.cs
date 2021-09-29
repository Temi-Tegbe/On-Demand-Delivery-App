using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.Base;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;

        public ProductsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration, IProductRepository productRepository)
        {
            _userManger = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _productRepository = productRepository;

        }

        [HttpPost]
        //[Authorize(Roles = "Customer")]
        [Route("register-product")]
        public async Task<IActionResult> RegisterProduct([FromBody] Product model)
        {
            Response responseBody = new Response();

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Location,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = await _userManger.CreateAsync(user, model.Location);
            if (!result.Succeeded)
            {
                responseBody.Message = "Product was not added successfully";
                responseBody.Status = "Failed";
                responseBody.Payload = null;
                return BadRequest(responseBody);
            }

            await _productRepository.AddAsync(model, user);

            if (!await _roleManager.RoleExistsAsync("Product"))
                await _roleManager.CreateAsync(new ApplicationRole() { Name = "Product" });

            if (await _roleManager.RoleExistsAsync("Product"))
                await _userManger.AddToRoleAsync(user, "Product");

            responseBody.Message = "Product was added succesfully.";
            responseBody.Status = "Success";
            responseBody.Payload = null;
            return Created($"/user/[user.Id]", responseBody);


        }

    }
}
