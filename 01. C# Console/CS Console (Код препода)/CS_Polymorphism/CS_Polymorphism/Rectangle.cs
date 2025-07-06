using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Polymorphism
{
    class Rectangle: Figure
    {
        protected int a, b;

        public Rectangle(int x, int y, int a, int b): base(x, y)
        {
            this.a = a;
            this.b = b;
        }

        public override double GetS()
        {
            return a * b;
        }

        public override void Print()
        {
            // вызов метода Print базового класса
            //base.Print();

            Console.WriteLine($"Rectangle. x = {x}, y = {y}, a = {a}, b = {b}");
        }
    }
}
