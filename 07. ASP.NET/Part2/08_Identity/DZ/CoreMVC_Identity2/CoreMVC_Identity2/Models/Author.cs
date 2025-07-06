using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Identity2.Models
{
    public partial class Authors
    {
        [Key]
        public string au_id { get; set; }

        [Required]
        public string au_lname { get; set; }

        [Required]
        public string au_fname { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool? Contract { get; set; }
        public string Photo { get; set; }
    }
}
