using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }

        public string VendorName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public List<Product> Products { get; set; }
    }
}
