using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    public class Car
    {
        [Key]
        public string car_id { get; set; }

        [DefaultValue(@"Resources\Curs Images\unknown.jpg")]
        [StringLength(50)]
        public string pathImg { get; set; }

        [StringLength(30)]
        public string brand { get; set; }

        [StringLength(30)]
        public string body_type { get; set; }

        public string registrDate { get; set; }

        public Owner own { get; set; }
    }
}
