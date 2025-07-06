using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CS_Serialization
{
    [Serializable]
    public class Human
    {
        // сериализация поля в виде атрибута
        [XmlAttribute]
        public string Name;

        // сериализация поля в виде атрибута
        [XmlAttribute]
        public string Surname;
        public int Age;

        public Address address;

        public Human()
        {
            Name = "Angela";
            Surname = "Bekett";
            Age = 34;
        }
        public Human(string name, string surname, int age, string city, string street, int house)
        {
            Name = name;
            Surname = surname;
            Age = age;
            address = new Address(city, street, house);
        }
    }
}
