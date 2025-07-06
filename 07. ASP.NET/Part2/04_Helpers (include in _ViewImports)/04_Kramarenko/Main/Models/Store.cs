using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("stores")]
    public partial class Store
    {
        public Store()
        {
            Sales = new HashSet<Sale>();
        }

        [Key]
        [Column("stor_id")]
        [StringLength(4)]
        public string StorId { get; set; }
        [Column("stor_name")]
        [StringLength(40)]
        public string StorName { get; set; }
        [Column("stor_address")]
        [StringLength(40)]
        public string StorAddress { get; set; }
        [Column("city")]
        [StringLength(20)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(2)]
        public string State { get; set; }
        [Column("zip")]
        [StringLength(5)]
        public string Zip { get; set; }

        [InverseProperty(nameof(Sale.Stor))]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
