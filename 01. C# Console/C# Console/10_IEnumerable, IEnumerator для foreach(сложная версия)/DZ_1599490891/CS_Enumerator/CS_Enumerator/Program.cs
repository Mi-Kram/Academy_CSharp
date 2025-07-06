using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Enumerator
{
    class Person
    {
        string FIO;
        int age;
        public Person(string f, int a)
        {
            FIO = f;
            age = a;
        }
        public void Print()
        {
            Console.WriteLine(@"Name: {0}, Age: {1}", FIO, age);
        }
    }

    class Group: IEnumerable, IEnumerator
    {
        int curpos;

        List<Person> people = new List<Person>();

        public Group()
        {
            people.Add(new Person("Alex", 12));
            people.Add(new Person("Ivan", 23));
            people.Add(new Person("Vasya", 42));
            people.Add(new Person("Anna", 54));
            people.Add(new Person("Masha", 19));

            curpos = people.Count;
        }

        // вернуть текущее значение коллекции, которая просматривается в foreach
        public object Current
        {
            get
            {
                Console.WriteLine("Current");
                return people[curpos];
            }
        }

        public IEnumerator GetEnumerator()
        {
            // вызов стандартного энумератора стандартной коллекции
            // return people.GetEnumerator();

            return this;
        }

        // переместить указатель на текущий элемент коллекции на 1 вперёд
        // и возвратить true, если такой существует (иначе false)
        public bool MoveNext()
        {
            Console.WriteLine("MoveNext");
            if (curpos > 0)
            {
                curpos--;
                return true;
            }
            else
            {
                // сброс счётчика
                curpos = people.Count;
                return false;
            }
        }

        // сбросить указатель на текущий элемент в начало коллекции
        public void Reset()
        {
            Console.WriteLine("Reset");
            curpos = people.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Group group = new Group();

            foreach(Person person in group)
            {
                person.Print();
            }

            Console.WriteLine();

            foreach (Person person in group)
            {
                person.Print();
            }
        }
    }
}
