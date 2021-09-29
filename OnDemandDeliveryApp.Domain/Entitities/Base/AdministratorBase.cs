using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities.Base
{
public  class AdministratorBase
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }


        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "This is not a valid email.")]
        [Required(ErrorMessage = "Email Address is required.")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Phone]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber1 { get; set; }
    }
}
