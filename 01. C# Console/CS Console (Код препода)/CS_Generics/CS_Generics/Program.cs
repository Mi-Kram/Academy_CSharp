using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Generics
{
    // обобщённый делегат (тип данных)
    delegate T MyDelegate<T>(T a, T b);

    // обобщённый интерфейс
    interface IGen<T>
    {
        void print(T t);
    }

    // обычный класс, реализовывающий обобщённый, но конкретизированный интерфейс
    class Test4 : IGen<int>
    {
        public void print(int t)
        {
            Console.WriteLine(t);
        }
    }

    // обычный класс, реализовывающий обобщённый интерфейс
    class Test5<T> : IGen<T>
    {
        public void print(T t)
        {
            Console.WriteLine(t);
        }
    }

    interface ITest3
    {
        void print();
    }

    class Test2 : ITest3
    {
        public int a = 3;
        public Test2()
        {
            a = 5;
        }
        public void print()
        {
            Console.WriteLine(a);
        }

        // обобщённый метод внутри обычного класса
        public T2 print2<T2, R> (R r2) where T2 : new()
        {
            T2 t2 = new T2();
            R r = default(R);
            return t2;
        }
    }

    class Program
    {
        string f(string a, string b)
        {
            return a + b;
        }

        int f2(int a, int b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            Vector<int> vector = new Vector<int>(10);
            vector[1] = 23;
            vector[3] = 12;
            vector.Print();

            Vector<string> vector2 = new Vector<string>(5);
            vector2[2] = "hello";
            vector2[3] = "world";
            vector2.Print();

            Console.WriteLine();

            UnitGroup<Soldier> soldiers = new UnitGroup<Soldier>();
            soldiers.AddUnit(new Soldier() { Name = "Alex", Age = 23, Health = 100, Speed = 5, Weight = 80});
            soldiers.AddNewUnit("Boris", 4, 89);
            soldiers.Print();

            UnitGroup<Tank> tanks = new UnitGroup<Tank>();
            tanks.AddUnit(new Tank() { Name = "Tiger", Health = 100, Speed = 30, Weight = 8000 });
            tanks.AddNewUnit("Pantera", 35, 90);
            tanks.Print();

            // НЕ РАБОТАЕТ с интерфейсами
            //UnitGroup<UnitGroup<IUnit>> army = new UnitGroup<UnitGroup<IUnit>>();

            // вызов обобщённого метода
            Test2 t2 = new Test2();
            t2.print2<int, int>(3);

            Program pr = new Program();

            // создание переменной типа MyDelegate и присвоение адреса метода
            MyDelegate<string> del1 = pr.f;
            del1 += new MyDelegate<string>(pr.f);

            string result = del1("abc", "bcd");
            Console.WriteLine(result);

            // создание переменной типа MyDelegate и присвоение адреса метода
            MyDelegate<int> del2 = pr.f2;
            del2 += new MyDelegate<int>(pr.f2);

            Console.WriteLine(del2(3, 5));
        }
    }
}
