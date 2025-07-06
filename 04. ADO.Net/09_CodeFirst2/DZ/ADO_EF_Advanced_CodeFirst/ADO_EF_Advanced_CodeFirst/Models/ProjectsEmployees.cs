using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class ProjectEmployees
    {
        [Key, Column(Order = 0)]
        public int ProjectId { get; set; }

        [Key, Column(Order = 1)]
        public int EmployeeId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
