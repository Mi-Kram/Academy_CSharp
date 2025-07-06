using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Restful_Client_Auth
{
    public class Author
    {
        public string Au_id { get; set; }
        public string au_fname { get; set; }
        public string au_lname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public bool? Contract { get; set; }
    }
}
