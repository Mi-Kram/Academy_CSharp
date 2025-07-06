using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<ProjectEmployees> ProjectEmployees { get; set; }
    }
}
