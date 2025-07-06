using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CS_Timer
{
    class Program
    {
        static System.Timers.Timer timer;
        static int n = 0;

        static void Main(string[] args)
        {
            timer = new System.Timers.Timer(2000);

            // интервал срабатывания таймера
            timer.Interval = 1000;

            // указать метод, который будет запускаться по таймеру
            timer.Elapsed += Timer_Elapsed;

            // запуск таймера
            timer.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"i = {i}");
                Thread.Sleep(150);
            }

            Console.WriteLine("Press any key for exit.");
            Console.ReadKey();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (n < 10)
            {
                // показать время срабатывания таймера
                Console.WriteLine("Timer tick: {0}, n = {1}", e.SignalTime.ToString(), n);
                n++;
            }
            else
            {
                // остановка таймера
                ((System.Timers.Timer)sender).Stop();
            }
        }
    }
}
