using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Polymorphism
{
    class Circle : Figure
    {
        protected int r;

        public Circle(int x, int y, int r) : base(x, y)
        {
            this.r = r;
        }

        public override void Print()
        {
            // вызов метода Print базового класса
            // base.Print();

            Console.WriteLine($"Circle. x = {x}, y = {y}, r = {r}");
        }

        public override double GetS()
        {
            return 3.14 * r*r;
        }
    }
}
