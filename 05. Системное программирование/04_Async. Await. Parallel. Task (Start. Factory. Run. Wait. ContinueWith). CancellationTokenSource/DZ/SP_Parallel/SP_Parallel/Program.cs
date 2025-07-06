using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Test
{
    class Program
    {
        static void Calculate(int x)
        {
            Console.WriteLine("Calculate {0} started.", x);
            Thread.Sleep(10000-x*1000);
            Console.WriteLine("Calculate {0} stopped.", x);
        }

        static void Calculate2(int x, ParallelLoopState pls)
        {
            // Выход из параллельного цикла по условию
            if (x == 3)
            {
                // Остановка параллельного запуска оставшихся методов
                pls?.Break();
            }

            Console.WriteLine("Calculate {0} started.", x);
            Thread.Sleep(10000 - x * 1000);
            Console.WriteLine("Calculate {0} stopped.", x);
        }

        static void ParallelMethod()
        {
            Console.WriteLine("ParallelMethod start.");
            Thread.Sleep(1700);
            Console.WriteLine("ParallelMethod end.");
        }

        static void Main(string[] args)
        {
            // Параллельное исполнение lambda-выражений
            /*Parallel.Invoke(
                ParallelMethod,
                () =>
                {
                    for (int i=0; i<20; i++)
                    {
                        Console.WriteLine("Task 1");
                        Thread.Sleep(700);
                    }  
                },
                () =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Console.WriteLine("Task 2");
                        Thread.Sleep(500);
                    }
                }
            );*/

            // 100% загрузка потоков
            /*Parallel.Invoke(
                () =>
                {
                    for (; ; );
                },
                () =>
                {
                    for (; ; );
                }
            );*/

            /*for(int i=1; i<10; i++)
            {
                Calculate(i);
            }*/

            // Параллельный цикл for для запуска метода 
            //Parallel.For(1, 10, Calculate);

            // Опции позволяют регулировать количество одновременно запущенных потоков (ядер)
            //Parallel.For(1, 10, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, Calculate);

            // Методы запускаются параллельно и им передаются значения из коллекции
            //ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 7, 2, 8, 5, 9 }, Calculate);

            ParallelLoopResult result = Parallel.For(1, 10, new ParallelOptions() { MaxDegreeOfParallelism = 5 }, Calculate2);

            // Если цикл был прерван внутри
            if (!result.IsCompleted)
                Console.WriteLine("Stop iteration: {0}", result.LowestBreakIteration);  // Показать номер прерывания цикла
            

            Console.ReadLine();
        }
    }
}
