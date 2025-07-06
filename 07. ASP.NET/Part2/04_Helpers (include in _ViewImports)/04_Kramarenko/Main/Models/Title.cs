using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("titles")]
    [Index(nameof(Title1), Name = "titleind")]
    public partial class Title
    {
        public Title()
        {
            Sales = new HashSet<Sale>();
            Titleauthors = new HashSet<Titleauthor>();
        }

        [Key]
        [Column("title_id")]
        [StringLength(6)]
        public string TitleId { get; set; }
        [Required]
        [Column("title")]
        [StringLength(80)]
        public string Title1 { get; set; }
        [Required]
        [Column("type")]
        [StringLength(12)]
        public string Type { get; set; }
        [Column("pub_id")]
        [StringLength(4)]
        public string PubId { get; set; }
        [Column("price", TypeName = "money")]
        public decimal? Price { get; set; }
        [Column("advance", TypeName = "money")]
        public decimal? Advance { get; set; }
        [Column("royalty")]
        public int? Royalty { get; set; }
        [Column("ytd_sales")]
        public int? YtdSales { get; set; }
        [Column("notes")]
        [StringLength(200)]
        public string Notes { get; set; }
        [Column("pubdate", TypeName = "datetime")]
        public DateTime Pubdate { get; set; }

        [ForeignKey(nameof(PubId))]
        [InverseProperty(nameof(Publisher.Titles))]
        public virtual Publisher Pub { get; set; }
        [InverseProperty(nameof(Sale.Title))]
        public virtual ICollection<Sale> Sales { get; set; }
        [InverseProperty(nameof(Titleauthor.Title))]
        public virtual ICollection<Titleauthor> Titleauthors { get; set; }
    }
}
