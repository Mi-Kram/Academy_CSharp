using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Static
{
    class Person
    {
        // статическое поле
        static int peopleCount = 0;

        // статическое свойство
        public static int PeopleCount 
        {
            get
            {
                return peopleCount;
            } 
        }

        // статический контруктор
        static Person()
        {
            peopleCount = 0;
            Console.WriteLine("Person static constuctor");
        }

        // обычный конструктор
        public Person()
        {
            peopleCount++;
            Console.WriteLine("Person constuctor");
        }

        // деструктор
        ~Person()
        {
            peopleCount--;
            Console.WriteLine("Person destuctor");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"People count: {Person.PeopleCount}");

            Person person = new Person();
            Console.WriteLine($"People count: {Person.PeopleCount}");

            Person person2 = new Person();
            Console.WriteLine($"People count: {Person.PeopleCount}");

            person = null;
            person2 = null;

            // принудительная очистка памяти из-под неиспользуемых объектов
            GC.Collect(0, GCCollectionMode.Forced);

            // подождать окончания очистки
            GC.WaitForFullGCComplete();

            Console.ReadLine();
            Console.WriteLine($"People count: {Person.PeopleCount}");
        }
    }
}
