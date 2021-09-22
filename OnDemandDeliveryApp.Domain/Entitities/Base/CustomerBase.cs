using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities.Base
{
   public class CustomerBase
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


      


        [MaxLength(200)]
        [Required(ErrorMessage = "Address is required.")]
        public string ResidentialAddress { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(20)]
        [Phone]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber1 { get; set; }

        [MaxLength(20)]
        [Phone]
        public string PhoneNumber2 { get; set; }


        [MaxLength(50)]
        [Required]
        public string StateOfOrigin { get; set; }

        [MaxLength(20)]
        [Required]
        public string Gender { get; set; }

        [MaxLength(50)]
        [Required]
        public string CountryOfOrigin { get; set; }
    }
}
