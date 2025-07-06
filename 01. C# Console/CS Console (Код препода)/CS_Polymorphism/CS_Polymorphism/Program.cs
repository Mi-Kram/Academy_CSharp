using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            //Figure figure = new Figure(1, 2);
            //figure.Print();

            //Figure figure = new Rectangle(1, 2, 4, 5);
            //figure.Print();

            //Figure figure2 = new Circle(1, 2, 3);
            //figure2.Print();

            Document doc = new Document();
            doc.Add(new Rectangle(1, 2, 4, 5));
            doc.Add(new Circle(1, 2, 3));
            doc.Add(new Rectangle(3, 6, 7, 8));
            doc.Print();
        }
    }
}
