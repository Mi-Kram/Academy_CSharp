using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Keyless]
    public partial class Titleview
    {
        [Required]
        [Column("title")]
        [StringLength(80)]
        public string Title { get; set; }
        [Column("au_ord")]
        public byte? AuOrd { get; set; }
        [Required]
        [Column("au_lname")]
        [StringLength(40)]
        public string AuLname { get; set; }
        [Column("price", TypeName = "money")]
        public decimal? Price { get; set; }
        [Column("ytd_sales")]
        public int? YtdSales { get; set; }
        [Column("pub_id")]
        [StringLength(4)]
        public string PubId { get; set; }
    }
}
