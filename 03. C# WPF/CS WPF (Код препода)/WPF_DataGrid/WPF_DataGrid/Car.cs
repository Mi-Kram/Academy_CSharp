using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid_Test
{
    public class Car
    {
        public string ImagePath { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }

        public DateTime BuyDate { get; set; }
        public int Price { get; set; }

        public bool IsForSale { get; set; }

        public override string ToString()
        {
            string result = String.Format(@"Brand: {0}, Model: {1}, Category: {2}", Brand, Model, Category);
            return result;
        }
    }
}
