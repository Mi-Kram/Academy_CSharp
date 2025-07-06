using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreMVC_RazorPages_CodeFirst2.Models
{
    [Table("MyGenre")]
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        public String Name { get; set; }
    }
}