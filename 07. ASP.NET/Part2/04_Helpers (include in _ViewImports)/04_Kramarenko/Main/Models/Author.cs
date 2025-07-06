using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("authors")]
    [Index(nameof(AuLname), nameof(AuFname), Name = "aunmind")]
    public partial class Author
    {
        public Author()
        {
            Titleauthors = new HashSet<Titleauthor>();
        }

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

        [InverseProperty(nameof(Titleauthor.Au))]
        public virtual ICollection<Titleauthor> Titleauthors { get; set; }
    }
}
