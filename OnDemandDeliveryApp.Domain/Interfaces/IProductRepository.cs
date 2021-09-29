using OnDemandDeliveryApp.Domain.Entitities;
using OnDemandDeliveryApp.Domain.Entitities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Interfaces.Base
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task AddAsync(Product productInfo, ApplicationUser userInfo);
    

    }
}
