﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Main.Models
{
    [Table("jobs")]
    public partial class Job
    {
        public Job()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("job_id")]
        public short JobId { get; set; }
        [Required]
        [Column("job_desc")]
        [StringLength(50)]
        public string JobDesc { get; set; }
        [Column("min_lvl")]
        public byte MinLvl { get; set; }
        [Column("max_lvl")]
        public byte MaxLvl { get; set; }

        [InverseProperty(nameof(Employee.Job))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
