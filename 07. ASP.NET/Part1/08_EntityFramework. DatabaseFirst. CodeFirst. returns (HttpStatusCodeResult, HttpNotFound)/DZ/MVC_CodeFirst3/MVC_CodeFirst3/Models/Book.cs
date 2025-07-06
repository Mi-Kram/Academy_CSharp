using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_CodeFirst3.Models
{
    [Table("MyBook")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public String Title { get; set; }

        [Required]
        public String Author { get; set; }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        public DateTime PubDate { get; set; }

        public double Price { get; set; }

        public int Pages { get; set; }
    }
}