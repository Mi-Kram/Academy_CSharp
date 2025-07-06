using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Properties
{
    class Program
    {
        // передача параметров в метод по ссылке при помощи ключевого слова ref
        static void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        // метод, возвращающий несколько результатов при помощи ключевого слова out
        static void MinMax(int[] arr, out int min, out int max)
        {
            if (arr == null || arr.Length == 0)
                throw new Exception("Attempt to find min, max in empty array.");

            min = max = arr[0];
            foreach (var item in arr)
            {
                if (min > item)
                    min = item;
                if (max < item)
                    max = item;
            }
        }

        // метод с неопределённым количеством параметров
        static int sum(params int[] arr)
        {
            int res = 0;
            foreach (var item in arr)
            {
                res += item;
            }
            return res;
        }

        static void Main(string[] args)
        {
            /*int resMin = 0, resMax = 0;

            MinMax(new int[] { 9, 8, 7, 6, 5 }, out resMin, out resMax);

            Console.WriteLine($"Min: {resMin}, Max: {resMax}");

            int n = 7, m = 5;
            swap(ref n, ref m);
            Console.WriteLine($"n: {n}, m: {m}");

            //int result = sum(new int[] { 1, 2, 3, 4, 5 });
            int result = sum(1, 2, 3);
            Console.WriteLine($"Sum = {result}");*/

            /*Student st = new Student("Alex Petrov", 23, 180, 67);
            st.Age = 90;
            st.Print();
            int aa = st.Age;

            Car car = new Car() { Brand = "BMW", Model = "Z8", Price = 120000, Speed = 250 };
            Console.WriteLine(car);*/

            Matrix matrix = new Matrix(3, 4);
            matrix[0, 0] = 100;
            matrix.Print();
        }
    }
}
