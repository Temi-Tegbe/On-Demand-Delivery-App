using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.Base;
using OnDemandDeliveryApp.Domain.Interfaces.Base;
using OnDemandDeliveryApp.Infrastructure.Repositories;

namespace OnDemandDeliveryApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly OnDemandDeliveryDbContext _context;
        public ProductRepository(OnDemandDeliveryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Product registrationInfo, ApplicationUser userInfo)

        {
            _context.Products.Add(
                new Product
                {
                    Location = registrationInfo.Location
                }
                );

            await _context.SaveChangesAsync();
        }
    }
    
}
