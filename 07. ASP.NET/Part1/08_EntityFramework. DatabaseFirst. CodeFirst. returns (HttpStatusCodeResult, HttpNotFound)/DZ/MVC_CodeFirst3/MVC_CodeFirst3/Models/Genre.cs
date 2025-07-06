using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CodeFirst3.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public String Name { get; set; }
    }
}