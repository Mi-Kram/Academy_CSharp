using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class Owner
    {
        // pathImg varchar(100), name varchar(50), address varchar(50), phone varchar(20)
        [Key]
        public string  own_id{ get; set; }

        [DefaultValue(@"Resources\Owners Images\unknown.jpg")]
        [StringLength(100)]
        public string pathImg { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(20)]
        public string phone { get; set; }
    }
}
