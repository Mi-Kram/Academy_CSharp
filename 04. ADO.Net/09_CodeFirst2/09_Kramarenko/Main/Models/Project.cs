using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class Project
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        [StringLength(20)]
        public string startDate { get; set; }

        [StringLength(20)]
        public string endDate { get; set; }

        [StringLength(200)]
        [DefaultValue("Не указано")]
        public string description { get; set; }
    }
}
