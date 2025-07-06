using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Polymorphism
{
    class Document
    {
        // список различных геометрических фигур
        List<Figure> figures = new List<Figure>();

        public void Add(Figure figure)
        {
            figures.Add(figure);
        }

        public void Print()
        {
            foreach (var figure in figures)
            {
                figure.Print();
            }
        }

        public double GetS()
        {
            double result = 0;
            foreach (var figure in figures)
            {
                result += figure.GetS();
            }

            return result;
        }
    }
}
