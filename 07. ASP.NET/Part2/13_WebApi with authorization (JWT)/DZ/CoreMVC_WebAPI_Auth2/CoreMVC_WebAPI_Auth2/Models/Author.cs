using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreMVC_WebAPI_Auth2.Models
{
    public partial class Author
    {
        [Key]
        public string au_id { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "You need to specify the last name")]
        public string au_lname { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "You need to specify the first name")]
        public string au_fname { get; set; }
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "You need to specify the address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You need to specify the city")]
        [MaxLength(100, ErrorMessage = "City name is too long!")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "You need to specify the state")]
        [MaxLength(100)]
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Contract { get; set; }
    }
}
