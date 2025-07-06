using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreMVC_RazorPages_CodeFirst2.Models
{
    [Table("MyBook")]
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(5)]
        public String Title { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        public String Author { get; set; }

        public Genre Genre { get; set; }

        public byte GenreId { get; set; }

        public DateTime PubDate { get; set; }

        [Required]
        public int Price { get; set; }
    }
}