using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnDemandDeliveryApp.Domain.Entitities
{
  public  class Product
    {
        [Key]
        [Required]
        public long ProductId { get; set; }

        [Required]
        public long UserId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Product location is required")]
        public string Location { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
