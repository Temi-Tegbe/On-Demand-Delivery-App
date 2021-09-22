using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
  public  class DispatchedProduct
    {

        public long ProductId { get; set; }

        public Guid DispatcherId { get; set; } 

        public Product AssignedProduct { get; set; }

        public Dispatcher AssignedDispatcher { get; set; }
    }
}
