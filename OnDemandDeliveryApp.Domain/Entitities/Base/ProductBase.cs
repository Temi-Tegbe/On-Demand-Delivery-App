using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities.Base
{
  public  class ProductBase
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Product location is required")]
        public string Location { get; set; }
    }
}
