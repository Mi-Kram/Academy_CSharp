using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Operators
{
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading

    class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator ++(Point p)
        {
            return new Point(++p.x, ++p.y);
        }
        public static Point operator --(Point p)
        {
            return new Point(--p.x, --p.y);
        }

        // неявное приведение типа
        public static implicit operator int(Point p)
        {
            return p.x + p.y;
        }

        // явное приведение типа
        /*public static explicit operator int(Point p)
        {
            return p.x - p.y;
        }*/

        public static int operator -(Point p)
        {
            return -p.x;
        }
        public static int operator +(Point p)
        {
            return p.x;
        }
        public static bool operator !(Point p)
        {
            return p.x == 0 || p.y == 0;
        }
        public static Point operator +(Point p, int n)
        {
            Point res = new Point(p.x, p.y);
            res.x = p.x + n;
            res.y = p.y + n;
            return res;
        }
        public static Point operator -(Point p, int n)
        {
            Point res = new Point(p.x, p.y);
            res.x = p.x - n;
            res.y = p.y - n;
            return res;
        }
        public static Point operator +(Point p, Point p2)
        {
            p.x += p2.x;
            p.y += p2.y;
            return p;
        }
        public static Point operator -(Point p, Point p2)
        {
            p.x -= p2.x;
            p.y -= p2.y;
            return p;
        }

        public static bool operator ==(Point p, Point p2)
        {
            return (p.x == p2.x && p.y == p2.y);
        }
        public static bool operator !=(Point p, Point p2)
        {
            return (p.x != p2.x || p.y != p2.y);
        }

        public static bool operator >(Point p, Point p2)
        {
            return (p.x > p2.x && p.y > p2.y);
        }
        public static bool operator <(Point p, Point p2)
        {
            return (p.x < p2.x && p.y < p2.y);
        }
        public static bool operator true(Point p)
        {
            Console.WriteLine("Operator true");
            return (p.x != 0 || p.y != 0);
        }
        public static bool operator false(Point p)
        {
            Console.WriteLine("Operator false");
            return (p.x == 0 && p.y == 0);
        }

        // метод, который позволяет правильно сравнивать объекты классов
        public override bool Equals(object obj)
        {
            try
            {
                Point b = (Point)obj;
                return this.x == b.x && y == b.y;
            }
            catch
            {
                return false;
            }
        }

        // метод запускается при добавление объекта класса в словари и в множетсва
        public override int GetHashCode()
        {
            //return base.GetHashCode();

            int hash = 17;
            hash = (hash * 7) + x.GetHashCode();
            hash = (hash * 7) + y.GetHashCode();
            return hash;
        }

        public void print()
        {
            Console.WriteLine(x);
            Console.WriteLine(y);

        }
    }
    class Program
    {
        static void Main()
        {
            Point p = new Point(3, 5);
            Point p2 = new Point(3, 5);

            //p++;

            // оператор -= был сгенерирован автоматически, потому что был переопределён бинарный оператор -
            //p += 3;

            // запуск бинарного оператора +
            //p = p + p2;

            //p.print();

            // запуск оператора ++ и бинарного оператора -
            //Point p3 = p++ - 1;

            //p3.print();

            // запуск неявного оператора приведения типа int
            //Console.WriteLine(p3);

            //p3.print();
            //p.print();

            // запуск оператора ==
            /*if (p == p2)
                Console.WriteLine("Equals");
            else Console.WriteLine("NOT equals!!!");*/

            // запуск оператора <
            Console.WriteLine(p<p2);

            // запуск явного оператора int
            /*int n = (int)p;
            Console.WriteLine(n);*/
            
            //запуск оператора true
            if (p) 
                Console.WriteLine("OK!!!");
            else Console.WriteLine("NOT OK!!!");
        }
    }
}
