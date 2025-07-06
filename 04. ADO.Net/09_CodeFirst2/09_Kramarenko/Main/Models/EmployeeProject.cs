using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class EmployeeProject
    {
        //[Key, Column("employeeID")]
        //public Employee Employee { get; set; }

        //[Key, Column("projectID")]
        //public Project Project { get; set; }



        [Key, Column(Order = 0)]
        public int EmployeeId { get; set; }

        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }




        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
