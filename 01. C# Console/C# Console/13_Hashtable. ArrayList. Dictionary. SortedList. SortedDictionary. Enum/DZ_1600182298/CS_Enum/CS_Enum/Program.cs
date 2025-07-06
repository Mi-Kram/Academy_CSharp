using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Enum
{
    enum Cars
    {
        BMW,
        Opel = 2,
        Audi,
        Nissan = 4,
        Mercedes = 5,
        Merc = 5,
        MercedesBenz = Mercedes
    }

    enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulo,
        Exponent
    }

    class Test1
    {
        public enum Cities
        {
            Moskow, Donetsk, Piter, Rostov, Odessa
        }
        public enum Planes: short
        {
            Boeing, AirBus, Tu
        }
    }

    class Program
    {
        static double Calculate(double x, double y, Operation op)
        {
            switch(op)
            {
                case Operation.Add:
                    return x + y;
                case Operation.Subtract:
                    return x - y;
                case Operation.Multiply:
                    return x * y;
                case Operation.Divide:
                    return x / y;
                case Operation.Modulo:
                    return x % y;
                case Operation.Exponent:
                    return Math.Pow(x, y);
                default:
                    return 1;
            }
        }

        static void Main(string[] args)
        {
            Cars cars = Cars.Audi;

            Console.WriteLine(cars.ToString());
            Console.WriteLine((int)cars);

            Console.WriteLine(Calculate(2, 3, Operation.Exponent));
        }
    }
}
