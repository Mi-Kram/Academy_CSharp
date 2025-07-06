using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Serialization
{
    [Serializable]
    public class Student : Human
    {
        public string Phone;

        // XML-сериализация сохраняет только открытые поля
        string ttt;

        public List<int> marks = new List<int>();

        Dictionary<char, long> dict = new Dictionary<char, long>
            {
                { 'A', 11 },
                { 'B', 22 },
                { 'C', 33 }
            };

        public Student(string name, string surname, int age, string city, string street, int house) : base(name, surname, age, city, street, house)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Phone = "457-78-90";
            ttt = "ok";

            marks.Add(5);
            marks.Add(7);
            marks.Add(9);
        }
        public Student() : base()
        {
            Phone = "457-78-90";
            ttt = "ok";
        }
        public void Print()
        {
            Console.WriteLine($"First name: {Name}, Last name: {Surname}, Age: {Age}, Address: {this.address.City}, {this.address.Street}, {this.address.House}");
            
            foreach (var mark in marks)
                Console.Write($"{mark} ");
            Console.WriteLine();

            foreach (var item in dict)
            {
                Console.WriteLine(@"{0} : {1}", item.Key, item.Value);
            }
        }
    }
}
