using OnDemandDeliveryApp.Domain.Entitities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
public  class Administrator : AdministratorBase
    {
        [Key]
        [Required]
        public long AdministratorId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        public ApplicationUser User { get; set; }


    }
}
