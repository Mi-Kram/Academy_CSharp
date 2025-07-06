using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreMVC_CodeFirst2
{
    [Table("MyGenre")]
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        public String Name { get; set; }
    }
}