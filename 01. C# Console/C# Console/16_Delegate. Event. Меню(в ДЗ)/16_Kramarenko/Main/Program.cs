using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO.Ports;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.Task1();
            Tasks.Task2();

            End();
        }

        static void End()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
        }
    }

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. При помощи делегатов реализовать сортировку с использованием компараторов ");
            Console.WriteLine("       для сортировки одномерных массивов в порядке возрастания и убывания       \n\n\n");
            Console.ResetColor();

            try
            {
                Vector v = new Vector(5);

                for (int i = 0; i < v.Length; i++)
                {
                    v[i] = i + 1;
                }

                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine('\n');

                v.Sort(v.descend);
                //v.Sort((int a, int b) => a > b);

                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine('\n');

                v.Sort(v.ascend);
                //v.Sort((int a, int b) => a < b);

                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }


            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. Реализовать при помощи events. Сервер содержит внутри себя таймер, который посылает всем подписчикам ");
            Console.WriteLine("       1 раз в сек. сообщения от текущей дате и времени. Первый подписчик печатает время на экран.          ");
            Console.WriteLine("       Второй подписчик пишет время в файл .log. Третий подписчик выключает сервер через 30 сек его работы  \n\n\n");
            Console.ResetColor();

            try
            {
                Server serv = new Server();
                serv.Start();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    class Vector
    {
        int[] arr;
        
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length) throw new Exception("False index");
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length) throw new Exception("False index");
                arr[index] = value;
            }
        }
        public int Length
        {
            get => arr.Length;
        }

        public Vector(int n)
        {
            if (n < 1) throw new Exception("Array size is less than 1");
            arr = new int[n];
        }

        public void Resize(int n)
        {
            if(n >= 0)
            {
                Array.Resize(ref arr, n);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                yield return arr[i];
            }
        }

        public delegate bool Compare(int a, int b);
        public void Sort(Compare comp)
        {
            QuickSort(ref arr, 0, arr.Length - 1, comp);
        }
        private void QuickSort(ref int[] arr, int start, int end, Compare comp)
        {
            if (start >= end) return;

            int i = start, j = end;
            int baseElement = (start + end) / 2;

            while (i < j)
            {
                int value = arr[baseElement];

                while (i < baseElement && comp(arr[i], value)) i++;
                while (j > baseElement && comp(value, arr[j])) j--;

                if (i < j)
                {
                    swap(ref arr[i], ref arr[j]);

                    if (i == baseElement) baseElement = j;
                    else if (j == baseElement) baseElement = i;
                } // if
            } // while

            QuickSort(ref arr, start, baseElement, comp);
            QuickSort(ref arr, baseElement + 1, end, comp);
        }
        private void swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        public bool ascend(int a, int b)
        {
            return a < b;
        }
        public bool descend(int a, int b)
        {
            return a > b;
        }
        public bool ClassComp(int a, int b)
        {
            if (a % 2 != 0 && b % 2 != 0) return a < b;
            if (a % 2 == 0 && b % 2 == 0) return a > b;

            if (a % 2 == 0 && b % 2 != 0) return false;
            if (a % 2 != 0 && b % 2 == 0) return true;

            return false;
        }
        public bool ClassComp2(int a, int b)
        {
            bool first = IsSimple(a);
            bool second = IsSimple(b);

            if (first && second) return a < b;
            if (!first && !second) return a > b;

            if (first && !second) return true;
            if (!first && second) return false;

            return false;
        }

        private bool IsSimple(int a)
        {
            if (a <= 1) return false;

            for (int i = 2; i < a; i++)
            {
                if (a % i == 0) return false;
            }

            return true;

            #region Хранит простые числа до числа a
            //if (a <= 1) return false;
            //List<int> simpleArr = new List<int>();
            //for (int i = 2; i < a; i++)
            //{
            //    bool flag = true;
            //    foreach (var item in simpleArr){if (i % item == 0) { flag = false; break; }}
            //    if (flag) simpleArr.Add(i);
            //}
            //foreach (var item in simpleArr){if (a % item == 0) return false;}
            //return true;
            #endregion
        }

    }

    class Server
    {
        Timer tmr;
        delegate void Subscriber(DateTime dt);
        event Subscriber Sub;

        bool turn;

        public Server()
        {
            tmr = new Timer(1000);
            tmr.Elapsed += Tmr_Elapsed;

            Sub += First;
            Sub += Second;
            Sub += Third;

            turn = false;
        }

        public void Start()
        {
            turn = true;

            tmr.Start();

            while (turn) { };
            tmr.Stop();
        }
        public void Stop()
        {
            turn = false;
        }

        private void First(DateTime dt)
        {
            Console.WriteLine(dt);
        }
        private void Second(DateTime dt)
        {
            try
            {
                if (!File.Exists("file.log")) File.Create("file.log");

                File.AppendAllText("file.log", (dt.ToString() + "\r\n"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        int count = 0; 
        private void Third(DateTime dt)
        {

            if(++count == 3)
            {
                count = 0;
                Stop();
            }
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            Sub?.Invoke(DateTime.Now);
        }
    }
}
