using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    abstract class People
    {
        protected string name;
        protected double age;

        public People(string name, double age)
        {
            this.name = name;
            this.age = age;
        }

        public abstract void Print();
    }

    class Worker : People
    {
        protected double salary;

        public Worker(string name, double age, double salary) : base(name, age)
        {
            this.salary = salary;
        }

        public double Salary { get => salary; }

        public override void Print()
        {
            Console.WriteLine("   Worker");
            Console.WriteLine($"name> {name}");
            Console.WriteLine($"age> {age}");
            Console.WriteLine($"salary> {salary}");
        }
    }

    class Freelancer : People
    {
        protected double moneyPerHour;
        protected int hours;

        public Freelancer(string name, double age, double moneyPerHour, int hours) : base(name, age)
        {
            this.moneyPerHour = moneyPerHour;
            this.hours = hours;
        }

        public double Salary { get => moneyPerHour * hours; }

        public override void Print()
        {
            Console.WriteLine("   Freelancer");
            Console.WriteLine($"name> {name}");
            Console.WriteLine($"age> {age}");
            Console.WriteLine($"salary> {moneyPerHour * hours}");
        }
    }

    class Manager : People
    {
        protected double salary;
        protected double bonus;

        public Manager(string name, double age, double salary, double bonus) : base(name, age)
        {
            this.salary = salary;
            this.bonus = bonus;
        }

        public double Salary { get => salary + bonus; }

        public override void Print()
        {
            Console.WriteLine("   Manager");
            Console.WriteLine($"name> {name}");
            Console.WriteLine($"age> {age}");
            Console.WriteLine($"salary> {salary + bonus}");
        }
    }


    class PeopleList
    {
        List<People> lst;

        
        public PeopleList()
        {
            lst = new List<People>();
        }
        public PeopleList(People people)
        {
            lst.Add(people);
        }

        public void Add(People people)
        {
            lst.Add(people);
        }
        public void Print()
        {
            foreach (var item in lst)
            {
                item.Print();
                Console.WriteLine('\n');
            }
        }
    }
}
