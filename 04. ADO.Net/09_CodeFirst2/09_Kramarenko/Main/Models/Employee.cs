using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int age { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(20)]
        public string phone{ get; set; }
    }
}
