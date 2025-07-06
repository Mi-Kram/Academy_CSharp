using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("publishers")]
    public partial class Publisher
    {
        public Publisher()
        {
            Employees = new HashSet<Employee>();
            Titles = new HashSet<Title>();
        }

        [Key]
        [Column("pub_id")]
        [StringLength(4)]
        public string PubId { get; set; }
        [Column("pub_name")]
        [StringLength(40)]
        public string PubName { get; set; }
        [Column("city")]
        [StringLength(20)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(2)]
        public string State { get; set; }
        [Column("country")]
        [StringLength(30)]
        public string Country { get; set; }

        [InverseProperty("Pub")]
        public virtual PubInfo PubInfo { get; set; }
        [InverseProperty(nameof(Employee.Pub))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Title.Pub))]
        public virtual ICollection<Title> Titles { get; set; }
    }
}
