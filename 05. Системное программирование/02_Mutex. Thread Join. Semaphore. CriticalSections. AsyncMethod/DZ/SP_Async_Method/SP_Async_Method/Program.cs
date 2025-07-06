using System;
using System.Threading;

namespace SP_Async_Method
{
    class Program
    {
        // тип данных для запуска метода не в основном потоке
        delegate double MyDelegate(int maxValue);

        static void Main(string[] args)
        {
            // занести адрес запускаемого метода
            MyDelegate del = CalculateSum;

            // Запустить CalculateSum в отдельном потоке 
            // maxValue, 
            // CalculateDone - функция запустится по окнчанию работы в отдельном потоке
            // опциональный параметр - null
            IAsyncResult res = del.BeginInvoke(500, CalculateDone, null);

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Main... {i}");
                Thread.Sleep(1000);
            }

            while(!res.IsCompleted)
            {
                Console.WriteLine("Still working");
                Thread.Sleep(1000);
            }

            // ожидание результата работы метода в отдельном потоке
            double sum = del.EndInvoke(res);

            Console.WriteLine("Sum: {0}", sum);

            Console.ReadKey();
        }

        static double CalculateSum(int maxValue)
        {
            Console.WriteLine("Start...");
            double sum = 0.0;
            for (int i = 1; i < maxValue; ++i)
            {
                sum += Math.Sqrt(i);
                Thread.Sleep(20);
            }
            return sum;
        }

        // метод запускается, когда параллельный вызов окончен
        static void CalculateDone(object state)
        {
            Console.WriteLine("End of work");
        }
    }
}
