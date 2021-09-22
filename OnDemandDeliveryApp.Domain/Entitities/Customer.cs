using OnDemandDeliveryApp.Domain.Entitities.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
   public  class Customer : CustomerBase
    {
        
       
        [Key]
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DateRegistered { get; set;}

        
      

    }
}
