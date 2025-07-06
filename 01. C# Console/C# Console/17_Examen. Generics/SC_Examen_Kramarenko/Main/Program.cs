using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tasks.Task1();
            //Tasks.Task2();
        }

    }

    class Tasks
    {
        public static void Task1()
        {
            try
            {
                FileText txt = new FileText("11111. 22222! 33333? 44444... 55555");
                txt.Print();
                Console.WriteLine('\n');


                Writer("Add(\"666\")");
                txt.Add("666");
                txt.Print();
                Console.WriteLine('\n');


                Writer("Insert(2, \"TEST\")");
                txt.Insert(2, "TEST");
                txt.Print();
                Console.WriteLine('\n');


                Writer("RemoveAt(2)");
                txt.RemoveAt(2);
                txt.Print();
                Console.WriteLine('\n');


                Writer("Set(\"qqqq.wwwww.rrrr\")");
                txt.Set("qqqq.wwwww.rrrr");
                txt.Print();
                Console.WriteLine('\n');


                Writer("this[int]");
                for (int i = 0; i < txt.Length; i++)
                {
                    Console.WriteLine(txt[i]);
                }
                Console.WriteLine('\n');


                Writer("this[int,int]");
                for (int i = 0; i < txt.Length; i++)
                {
                    for (int j = 0; j < txt.WordsInSentence(i); j++)
                    {
                        Console.Write($"{txt[i, j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine('\n');


                Writer("foreach");
                foreach (var item in txt)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine('\n');


                Writer("Load(string path)");
                txt.Load(@"config.txt");
                txt.Print();
                Console.WriteLine('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("\n\n");
        }
        public static void Task2()
        {
            Server serv = new Server(@"C:\Users\students\Desktop\SC_Examen_Kramarenko\Main\SERVER");
            serv.Start();

            Console.WriteLine("\n\n");
        }



        static void Writer(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(Message);
            Console.ResetColor();
        }

    }
}
