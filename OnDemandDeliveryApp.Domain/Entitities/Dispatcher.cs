using OnDemandDeliveryApp.Domain.Entitities.Base;
using OnDemandDeliveryApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
   public  class Dispatcher : DispatcherBase
    {

        [Key]
        [Required]
        public long DispatcherId { get; set; }

        [Required]
        public long UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

    }
}
