using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testr.Infrastructure.Repositories;

namespace OnDemandDeliveryApp.Infrastructure.Repositories
{
   public  class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly OnDemandDeliveryDbContext _context;

        public CustomerRepository(OnDemandDeliveryDbContext context) : base(context)

        {
            _context = context;
        }

        public async Task AddAsync(CustomerRegistration registrationInfo, ApplicationUser userInfo)
        {
            _context.Customers.Add(
                new Customer
                {
                    FirstName = registrationInfo.FirstName,
                    LastName = registrationInfo.LastName,
                    Email = registrationInfo.Email,
                    ResidentialAddress = registrationInfo.ResidentialAddress,
                    DateOfBirth = registrationInfo.DateOfBirth,
                    PhoneNumber1 = registrationInfo.PhoneNumber1,
                    PhoneNumber2 = registrationInfo.PhoneNumber2,
                    StateOfOrigin = registrationInfo.StateOfOrigin,
                    Gender = registrationInfo.Gender,
                    CountryOfOrigin = registrationInfo.CountryOfOrigin,
                    DateRegistered = DateTime.Now,
                    User = userInfo

                }
                );

            await _context.SaveChangesAsync();
        }
    }
}
