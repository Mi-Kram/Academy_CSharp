using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SP_Thread_Join
{
    class Program
    {
        Thread t1 = null;

        void thread1()
        {
            for (int i = 0; i<15; i++)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(800);
            }
        }

        void Test()
        {
            ThreadStart p1 = new ThreadStart(thread1);
            t1 = new Thread(p1);
            t1.IsBackground = true;

            // старт рабочего потока
            t1.Start();

            for (int i = 0; i<5; i++)
            {
                Console.WriteLine("Main");

                // сделать паузу на 0.9 сек
                Thread.Sleep(900);
            }

            // В этой точке основной поток подождёт окончания выполнения потока t1
            t1.Join();

            Console.WriteLine("Main end.");
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Test();
        }
    }
}
