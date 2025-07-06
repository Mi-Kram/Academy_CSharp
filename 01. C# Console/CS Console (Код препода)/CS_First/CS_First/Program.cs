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
            /*int[,] a = new int[2, 3];
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
            }*/

            /*
            Random rand = new Random();

            // зубчатые (рваные) массивы
            int[][] a = new int[10][];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = new int[rand.Next(10)+1];

                for (int k = 0; k < a[i].Length; k++)
                {
                    a[i][k] = i + k + 1;
                }
            }

            for (int i = 0; i < a.Length; i++)
            {
                for (int k = 0; k < a[i].Length; k++)
                {
                    Console.Write($"{a[i][k]}, ");
                }
                Console.WriteLine();
            }*/

            /*
            

            for (int i = 0; i < 10; i++)
            {
                // получить случайные числа в диапазоне [-5; -1]
                Console.WriteLine(rand.Next(5) - 5);
            }

            byte[] arr = new byte[10];

            // заполнить массив типа byte значениями в диапазоне [0; 255]
            rand.NextBytes(arr);

            foreach (var item in arr)
            {
                Console.Write($"{item}, ");
            }
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                // получить случайные числа в диапазоне [0.0; 1.0)
                Console.WriteLine(rand.NextDouble());
            }*/

            /*
            // получить текущие дату и время
            DateTime dt = DateTime.Now;
            Console.WriteLine(dt);
            Console.WriteLine($"Year: {dt.Year}, Month: {dt.Month}, Day: {dt.Day}");
            Console.WriteLine($"Hours: {dt.Hour}, Minutes: {dt.Minute}, Seconds: {dt.Second}");
            Console.WriteLine($"Milliseconds: {dt.Millisecond}, DayOfWeek: {dt.DayOfWeek}, DayOfYear: {dt.DayOfYear}");

            DateTime dt2 = new DateTime(1998, 12, 23, 12, 23, 9);

            // получить отрезок времени между двумя датами
            TimeSpan timeSpan = dt - dt2;
            Console.WriteLine($"Days: {timeSpan.TotalDays}");
            */

            /*
            // вычисление времени работы программы

            // засечь время в начале работы программы
            DateTime dt3 = DateTime.Now;

            // сама программа, время работы которой мы меряем
            Random rand2 = new Random();
            for (int i = 0; i < 10000; i++)
            {
                Console.WriteLine(rand2.Next(100));
            }

            // засечь время в конце работы программы
            DateTime dt4 = DateTime.Now;

            // вычислить время работы программы
            TimeSpan timeSpan2 = dt4 - dt3;
            Console.WriteLine($"Total milliseconds: {timeSpan2.TotalMilliseconds}");
            */

            /*
            string str = "Hello world.123,!!!";
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write(@"{0} ", str[i]);
            }
            Console.WriteLine();
            
            // получить подстроку
            Console.WriteLine($"Substring: {str.Substring(2, 3)}");

            // разделение на подстроки
            Console.WriteLine("Split: ");
            string[] words = str.Split(' ', '.', ',');
            foreach (var currentWord in words)
            {
                Console.WriteLine(currentWord);
            }

            // объединение в одну строку из массива строк
            string result = String.Join("|", words);
            Console.WriteLine($"Result: {result}");
            */

            /*string str = "Hello world";
            str = str + "!!!";
            Console.WriteLine(str);*/

            // изменяемая строка
            StringBuilder stringBuilder = new StringBuilder("Hello world");

            stringBuilder.Append(23);
            stringBuilder.Append(123.234);
            stringBuilder.Remove(0, 3);
            stringBuilder.Insert(0, 999);
            stringBuilder.Replace('1', '!');
            stringBuilder.Replace("999", "ok");

            string result = stringBuilder.ToString();
            Console.WriteLine(result);
        }
    }
}
