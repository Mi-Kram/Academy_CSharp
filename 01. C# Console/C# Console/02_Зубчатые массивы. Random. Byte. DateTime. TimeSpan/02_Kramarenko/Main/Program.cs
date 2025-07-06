using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        delegate void MyDel();
        static void Main(string[] args)
        {
            Console.Title = "HomeWork 02";

            //Tasks.Task1();
            //Tasks.Task2();
            //Tasks.Task3();
            //Tasks.Task4();

            #region Test

            unsafe
            {
                int task = 1;
                MyDel handler;

                switch (task)
                {
                    case 1:
                        handler = new MyDel(Tasks.Task1);
                        break;
                    case 2:
                        handler = new MyDel(Tasks.Task2);
                        break;
                    case 3:
                        handler = new MyDel(Tasks.Task3);
                        break;
                    case 4:
                        handler = new MyDel(Tasks.Task4);
                        break;
                    default: return;
                }

                while (true)
                {
                    handler();
                    if (handler == Tasks.Task4)
                    {
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }

            #endregion
        }
    }
}
