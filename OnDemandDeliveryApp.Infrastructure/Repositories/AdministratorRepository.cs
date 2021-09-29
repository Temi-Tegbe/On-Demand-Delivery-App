using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.DTOs;
using OnDemandDeliveryApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnDemandDeliveryApp.Infrastructure.Repositories;

namespace OnDemandDeliveryApp.Infrastructure.Repositories
{
  public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {
        private readonly OnDemandDeliveryDbContext _context;

        public AdministratorRepository(OnDemandDeliveryDbContext context) : base(context)
        {
            _context = context;
        }

       

        public async Task AddAsync(AdministratorRegistration registrationInfo, ApplicationUser userInfo)

        {
            
            _context.Administrators.Add(
                new Administrator
                {
                    FirstName = registrationInfo.FirstName,
                    LastName = registrationInfo.LastName,
                    Email = registrationInfo.Email,

                    PhoneNumber1 = registrationInfo.PhoneNumber1,
                    DateRegistered = DateTime.Now,
                    User = userInfo

                }
                );

            await _context.SaveChangesAsync();
        }
    }
}
