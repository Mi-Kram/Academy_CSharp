using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        [Column("Name")]
        [StringLength(50)]
        public string ManufacturerName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [DefaultValue("Unknown")]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string  City { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        public List<SmartPhone> SmartPhones { get; set; }
    }
}
