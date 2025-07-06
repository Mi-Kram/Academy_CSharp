using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    class Helicopter : IFly
    {
        public double this[int pos] => throw new NotImplementedException();

        public double Height => 0;

        public void Land()
        {
        }

        public void Move(double x, double y, double z)
        {
        }

        public void TakeOff()
        {
        }
    }
}
