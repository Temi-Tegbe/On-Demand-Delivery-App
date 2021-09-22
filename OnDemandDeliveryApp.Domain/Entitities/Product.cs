using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
  public  class Product
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }

        public string Category { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
    }
}
