using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("titleauthor")]
    [Index(nameof(AuId), Name = "auidind")]
    [Index(nameof(TitleId), Name = "titleidind")]
    public partial class Titleauthor
    {
        [Key]
        [Column("au_id")]
        [StringLength(11)]
        public string AuId { get; set; }
        [Key]
        [Column("title_id")]
        [StringLength(6)]
        public string TitleId { get; set; }
        [Column("au_ord")]
        public byte? AuOrd { get; set; }
        [Column("royaltyper")]
        public int? Royaltyper { get; set; }

        [ForeignKey(nameof(AuId))]
        [InverseProperty(nameof(Author.Titleauthors))]
        public virtual Author Au { get; set; }
        [ForeignKey(nameof(TitleId))]
        [InverseProperty("Titleauthors")]
        public virtual Title Title { get; set; }
    }
}
