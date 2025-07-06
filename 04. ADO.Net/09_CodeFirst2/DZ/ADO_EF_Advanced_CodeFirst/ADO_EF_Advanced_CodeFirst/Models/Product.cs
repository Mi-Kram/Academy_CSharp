using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_EF_CodeFirst
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public Vendor Vendor { get; set; }
    }
}
