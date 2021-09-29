using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Application.Helpers
{
   public class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly OnDemandDeliveryDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationHelper(IHttpContextAccessor context, OnDemandDeliveryDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _httpContext = context;
            _dbcontext = dbContext;
            _userManager = userManager;
        }

        private string FetchCurrentUserEmail()
        {
            string userEmail = string.Empty;

            //Extract the user's email from the request object

            Claim userEmailClaim = _httpContext.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();

            if (userEmailClaim != null)
                userEmail = userEmailClaim.Value;

            return userEmail;
        }

        public async Task<bool> CurrentUserHasRoleAsync(string roleName)
        {
            bool result = false;

            string userEmail = FetchCurrentUserEmail();
            ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
                result = await _userManager.IsInRoleAsync(user, roleName);

            return result;
        }


        public long GetCurrentCustomerId()
        {
            long result = 0;
            string userEmail = FetchCurrentUserEmail();

            //Look up the Customer's id using their email
            Customer customer = _dbcontext.Customers
                .Where(c => c.Email == userEmail)
                .FirstOrDefault();

            if (customer != null)
                result = customer.CustomerId;

            return result;
        }

        public long GetCurrentDispatcherId()
        {
            long result = 0;
            string userEmail = FetchCurrentUserEmail();

            //Look up the Customer's id using their email
            Dispatcher dispatcher = _dbcontext.Dispatchers
                .Where(c => c.Email == userEmail)
                .FirstOrDefault();

            if (dispatcher != null)
                result = dispatcher.DispatcherId;

            return result;
        }


    }
}
