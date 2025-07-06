using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CS_ExtensionMethods
{
    // при добавлении Extension Methods нужно создать именно static class
    public static class StringExt
    {
        // Extension Method имеет то имя, которое будет добавлено в класс-получатель
        // this - признак того, что это Extension Method
        // string - название класса-получателя
        // str - ссылка на текущий объект класса
        public static int VowelCount(this string str)
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ("AEUOIYaeuoiy".Contains(str[i]))
                    result++;
            }
            return result;
        }
    }

    public enum Car
    {
        BMW, Mercedes, Audi, Nissan, Opel, AstonMartin, VW, Ferrary, Subaru
    }

    // класс содержит метод расширения для enum
    public static class CarExt
    {
        public static IEnumerable GetValues(this Car car)
        {
            return Enum.GetValues(typeof(Car));
        }
    }

    // класс содержит метод расширения для кортежа
    public static class TupleExt
    {
        // метод расширения, который позволяет получить структуру Point из определённого кортежа
        public static Point ToPoint(this (double x, double y) self) => new Point(self.x, self.y);

        // метод расширения, который позволяет получить определённого кортеж из структуры Point
        public static (double x, double y) ToTuple(this Point self) => (self.X, self.Y);
    }

    class Program
    {
        static void Main(string[] args)
        {
            string s = "Hello world";
            Console.WriteLine($"Vowel Count: {s.VowelCount()}");

            Car car = Car.BMW;
            foreach (var item in car.GetValues())
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine();

            // создание экземпляра структуры Point
            var p = new Point(10, 20);
 
            // конвертирование структуры в кортеж
            var (x, y) = p.ToTuple();

            // конвертирование кортежа в структуру
            var p2 = (x, y).ToPoint();
        }
    }
}
