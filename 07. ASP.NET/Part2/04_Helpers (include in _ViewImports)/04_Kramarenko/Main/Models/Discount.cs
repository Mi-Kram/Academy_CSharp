using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Keyless]
    [Table("discounts")]
    public partial class Discount
    {
        [Required]
        [Column("discounttype")]
        [StringLength(40)]
        public string Discounttype { get; set; }
        [Column("stor_id")]
        [StringLength(4)]
        public string StorId { get; set; }
        [Column("lowqty")]
        public short? Lowqty { get; set; }
        [Column("highqty")]
        public short? Highqty { get; set; }
        [Column("discount", TypeName = "decimal(4, 2)")]
        public decimal Discount1 { get; set; }

        [ForeignKey(nameof(StorId))]
        public virtual Store Stor { get; set; }
    }
}
