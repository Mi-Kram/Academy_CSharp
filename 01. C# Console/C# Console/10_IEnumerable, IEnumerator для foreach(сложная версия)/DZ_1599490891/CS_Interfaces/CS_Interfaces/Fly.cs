using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    class Fly: IFly, IPrintable
    {
        public double Height => 0;

        public double this[int pos] => throw new NotImplementedException();

        public void Land()
        {

        }

        public void Move(double x, double y, double z)
        {

        }

        public void Print()
        {
        }

        public void TakeOff()
        {

        }

        public void Eat()
        {
            Console.WriteLine("Eat()");
        }
    }
}
