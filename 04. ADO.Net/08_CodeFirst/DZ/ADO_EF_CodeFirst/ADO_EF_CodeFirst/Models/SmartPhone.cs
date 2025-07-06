using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class SmartPhone
    {
        [Key]
        public int SmartPhoneId { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Price { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
