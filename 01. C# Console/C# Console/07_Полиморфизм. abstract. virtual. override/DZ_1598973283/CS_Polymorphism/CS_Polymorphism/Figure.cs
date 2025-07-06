using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Polymorphism
{
    abstract class Figure
    {
        protected int x, y;
        public Figure(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract double GetS();

        public virtual void Print()
        {
            Console.WriteLine($"X = {x}, Y = {y}");
        }
    }
}
