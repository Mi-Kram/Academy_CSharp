using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("pub_info")]
    public partial class PubInfo
    {
        [Key]
        [Column("pub_id")]
        [StringLength(4)]
        public string PubId { get; set; }
        [Column("logo", TypeName = "image")]
        public byte[] Logo { get; set; }
        [Column("pr_info", TypeName = "text")]
        public string PrInfo { get; set; }

        [ForeignKey(nameof(PubId))]
        [InverseProperty(nameof(Publisher.PubInfo))]
        public virtual Publisher Pub { get; set; }
    }
}
