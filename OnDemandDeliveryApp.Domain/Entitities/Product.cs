using OnDemandDeliveryApp.Domain.Entitities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
  public  class Product : ProductBase
    {
        [Key]
        [Required]
        public long ProductId { get; set; }

        [Required]
        public long UserId { get; set; }

        //public ApplicationUser User { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}
