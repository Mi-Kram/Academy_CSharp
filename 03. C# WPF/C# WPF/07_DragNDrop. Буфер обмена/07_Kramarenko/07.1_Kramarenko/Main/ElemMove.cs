using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class ElemMove
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double StepX { get; set; }
        public double StepY { get; set; }

        public ElemMove(double x, double y, double stepX, double stepY)
        {
            X = x;
            Y = y;

            StepX = stepX;
            StepY = stepY;
        }
    }
}
