using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    [Table("Vendors_Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Column("Name")]
        public string EmployeeName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [DefaultValue(23)]
        public int Age { get; set; }

        public ICollection<ProjectEmployees> ProjectEmployees { get; set; }
    }
}
