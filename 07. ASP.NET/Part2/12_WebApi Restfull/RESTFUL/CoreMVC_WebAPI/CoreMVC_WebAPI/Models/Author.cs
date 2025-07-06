using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoreMVC_WebAPI.Models
{
    [Table("authors")]
    public partial class Author
    {
        [Key]
        [Column("au_id")]
        [StringLength(11)]
        public string au_id { get; set; }
        [Required]
        [Column("au_lname")]
        [StringLength(40)]
        public string au_lname { get; set; }
        [Required]
        [Column("au_fname")]
        [StringLength(20)]
        public string au_fname { get; set; }
        [Required]
        [Column("phone")]
        [StringLength(12)]
        public string Phone { get; set; }
        [Column("address")]
        [StringLength(40)]
        public string Address { get; set; }
        [Column("city")]
        [StringLength(20)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(2)]
        public string State { get; set; }
        [Column("zip")]
        [StringLength(5)]
        public string Zip { get; set; }
    }
}
