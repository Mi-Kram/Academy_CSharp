using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("sales")]
    [Index(nameof(TitleId), Name = "titleidind")]
    public partial class Sale
    {
        [Key]
        [Column("stor_id")]
        [StringLength(4)]
        public string StorId { get; set; }
        [Key]
        [Column("ord_num")]
        [StringLength(20)]
        public string OrdNum { get; set; }
        [Column("ord_date", TypeName = "datetime")]
        public DateTime OrdDate { get; set; }
        [Column("qty")]
        public short Qty { get; set; }
        [Required]
        [Column("payterms")]
        [StringLength(12)]
        public string Payterms { get; set; }
        [Key]
        [Column("title_id")]
        [StringLength(6)]
        public string TitleId { get; set; }

        [ForeignKey(nameof(StorId))]
        [InverseProperty(nameof(Store.Sales))]
        public virtual Store Stor { get; set; }
        [ForeignKey(nameof(TitleId))]
        [InverseProperty("Sales")]
        public virtual Title Title { get; set; }
    }
}
