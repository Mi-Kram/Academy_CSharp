using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Serialization
{
    [Serializable]
    public class Address
    {
        public string City;
        public string Street;
        public int House;

        public Address()
        {
            City = "Moskow";
            Street = "Pushkina";
            House = 5;
        }

        public Address(string city, string street, int house)
        {
            City = city;
            Street = street;
            House = house;
        }
    }
}
