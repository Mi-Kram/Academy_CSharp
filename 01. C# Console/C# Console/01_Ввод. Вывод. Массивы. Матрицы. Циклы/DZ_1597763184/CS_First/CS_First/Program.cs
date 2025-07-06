using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_First
{
    class Program
    {


        static void Main(string[] args)
        {
            /*
            Console.Write("Enter your name, please: ");

            // ввод строки с консоли
            string name = Console.ReadLine();

            Console.Write("Enter your age, please: ");
            int age = 0;

            // ввод числа, который может выбросить исключение
            //age = Int32.Parse(Console.ReadLine());

            // ввод числа, который может выбросить исключение
            //age = Convert.ToInt32(Console.ReadLine());

            // безопасный ввод числа из консоли
            while (Int32.TryParse(Console.ReadLine(), out age) == false)
            {
                Console.WriteLine("Error in age. Try one more time.");
            }

            //Console.WriteLine("Hello, " + name + "!!!");
            //Console.WriteLine(@"Hello, {0}, your age is: {1}", name, age);
            Console.WriteLine($"Hello, {name}, your age is: {age}");
            */

            // одномерные массивы
            /*int[] a = new int[5];
            int[] b = new int[5] { 1, 2, 3, 4, 5 };
            int[] c = new int[] { 9, 8, 7, 6, 5 };
            int[] d = { 3, 4, 5, 6, 7 };

            int i = 0;
            for (; i < a.Length; i++)
            {
                Console.Write($"{a[i]}, ");
            }
            Console.WriteLine();

            i = 0;
            while(i<b.Length)
            {
                Console.Write($"{b[i++]}, ");
            }
            Console.WriteLine();

            i = 0;
            do
            {
                Console.Write($"{c[i++]}, ");
            } while (i < c.Length);
            Console.WriteLine();

            foreach(int current in d)
            {
                Console.Write($"{current}, ");
            }
            Console.WriteLine();*/

            // двумерные прямоугольные массивы
            int[,] a = new int[2, 3];
            int[,] b = new int[2, 3] { {1, 2, 3 }, { 4, 5, 6} };
            int[,] c = new int[, ] { { 9, 8, 7 }, { 6, 5, 4 } };
            int[,] d = { { 9, 8, 7 }, { 6, 5, 4 } };

            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int k = 0; k < b.GetLength(1); k++)
                {
                    Console.Write($"{b[i, k]}, ");
                }
                Console.WriteLine();
            }
        }
    }
}
