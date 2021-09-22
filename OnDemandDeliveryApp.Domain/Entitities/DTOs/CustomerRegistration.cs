using OnDemandDeliveryApp.Domain.Entitities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities.DTOs
{
    public class CustomerRegistration : CustomerBase
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
