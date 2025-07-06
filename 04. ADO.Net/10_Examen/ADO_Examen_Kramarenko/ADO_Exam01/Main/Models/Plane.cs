using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
    class Plane
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public double Speed { get; set; }
        public string ManufacturerId { get; set; }

        public Manufacture GetManufacture(List<Manufacture> mnfs)
        {
            foreach (Manufacture item in mnfs)
            {
                if (item.ManufacturerId == this.ManufacturerId) 
                    return item;
            }

            return null;
        }
    }
}

