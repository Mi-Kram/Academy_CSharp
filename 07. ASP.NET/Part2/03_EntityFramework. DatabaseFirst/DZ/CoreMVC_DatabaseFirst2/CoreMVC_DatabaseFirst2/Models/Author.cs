using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CoreMVC_DatabaseFirst2.Models
{
    [Table("authors")]
    [Index(nameof(AuLname), nameof(AuFname), Name = "aunmind")]
    public partial class Author
    {
        [Key]
        [Column("au_id")]
        [StringLength(11)]
        public string AuId { get; set; }
        [Required]
        [Column("au_lname")]
        [StringLength(40)]
        public string AuLname { get; set; }
        [Required]
        [Column("au_fname")]
        [StringLength(20)]
        public string AuFname { get; set; }
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
        [Column("contract")]
        public bool Contract { get; set; }
        [Column("img")]
        [StringLength(20)]
        public string Img { get; set; }
        [Column("photo")]
        [StringLength(50)]
        public string Photo { get; set; }
    }
}
