using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Tuples
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string name = null, string surname = null, int age = 0)
        {
            FirstName = name;
            LastName = surname;
            Age = age;
        }

        // стандартный метод, который позврляет присваивать объект текущего класса в кортеж с переменными
        public void Deconstruct(out string name, out string surname, out int age)
        {
            name = FirstName;
            surname = LastName;
            age = Age;
        }

        // оператор приведения типа из кортежа в Person
        public static implicit operator Person((string name, string surname, int age) x) 
            => new Person(x.name, x.surname, x.age);
        
        public void Print()
        {
            Console.WriteLine($"First name: {FirstName}, Last name: {LastName}, Age: {Age}");
        }
    }
    
    class Program
    {
        private static (int x, int y) PointOffset((int x, int y) point, int offsetX, int offsetY)
        {
            return (point.x + offsetX, point.y + offsetY);
        }

        static void Main(string[] args)
        {
            // объявление кортежа
            var tuple = (1, 2, 3);
            Console.WriteLine(tuple);

            // обращение к элементам
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(tuple.Item2);
            tuple.Item1 += 9;
            Console.WriteLine(tuple.Item1);

            Console.WriteLine(tuple);

            // объявление с указанием типов
            (string, string, int, int) address = ("Donetsk", "Lenina", 6, 12);
            address.Item3 = 9;
            Console.WriteLine(address);

            // объявление с указанием имён
            var address2 = (city: "Moskow", street: "Gorky park", house: 3, number: 5);
            Console.WriteLine(address2.city);
            Console.WriteLine(address2.street);

            // объявление с указанием типов и имён
            (string city, string street, int house, int number) address3 = ("Moskow", "Gorky park", 3, 5);
            address3.number = 7;
            Console.WriteLine(address3);

            // объявление кортежа из одиночных переменных
            var (firstName, lastName, height) = ("Alex", "Petrov", 180);
            firstName = "Ivan";
            Console.WriteLine(firstName);
            Console.WriteLine(lastName);
            Console.WriteLine(height);
            Console.WriteLine((firstName, lastName, height));

            // присваивание кортежей
            address = address2;

            // сравнение кортежей
            Console.WriteLine($"Tuple compare: {address == address2}");

            // передача и возвращение кортежа из метода
            var point = PointOffset((1, 2), 3, 4);
            Console.WriteLine($"Point with offset: {point}");

            // использование в коллекциях
            var p = (x: 1, y: 2);
            var hashSet = new HashSet<(int x, int y)>();
            hashSet.Add(p);
            hashSet.Add((45, 67));

            p.x++;
            Console.WriteLine(hashSet.Contains(p));

            var person = new Person("Alex", "Petrov", 34);

            // вызов метода Deconstruct класса Person
            var (name, surname, age) = person;
            
            // создание нового кортежа на основе переменных
            var alex = (name, surname, age);
            
            // вызов оператора приведения типов в классе Person
            Person person2 = alex;
            person2.Print();
        }
    }
}
