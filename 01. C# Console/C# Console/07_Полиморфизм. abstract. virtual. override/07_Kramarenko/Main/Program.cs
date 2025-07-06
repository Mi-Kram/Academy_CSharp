using System;
using System.Collections.Generic;
using System.IO;
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
            //Tasks.Task3();


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
            Console.WriteLine("Task1. Разработать класс, который позволяет генерировать простые числа при помощи индексатора. При ");
            Console.WriteLine("       обращении к индескатору переменной класса, возвращает простое число по порядку возрастания  \n\n\n");
            Console.ResetColor();

            try
            {
                PrimeNumbers pr = new PrimeNumbers();
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine($"pr[{i}] = {pr[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}\b!");
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
            Console.WriteLine("Task2. Разработать класс, который позволяет получать доступ к любому слову текстового файла по индексатору. ");
            Console.WriteLine("       Можно читать и менять слова. Слова отделены друг от друга пробелами и новыми строками                \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\07_Kramarenko\Main\text.txt";
                FileWords fw = new FileWords(path);

                for (int i = 0; i < fw.Length; i++)
                {
                    Console.WriteLine(fw[i]);
                }

                Console.WriteLine("\n\n");

                fw[0] = "GoodBuy";

                for (int i = 0; i < fw.Length; i++)
                {
                    Console.WriteLine(fw[i]);
                }

                {
                    fw[0] = "Hello";
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();

        }
        public static void Task3()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task3. Полиморфизм \n\n\n");
            Console.ResetColor();

            try
            {
                PeopleList pl = new PeopleList();

                pl.Add(new Worker("Alex", 42, 2000));
                pl.Add(new Freelancer("Tima", 26, 500, 7));
                pl.Add(new Manager("Vladimir", 33, 1700, 1200));

                pl.Print();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();

        }

        #region For Task1

        class PrimeNumbers
        {
            public int this[int n]
            {
                get
                {
                    if (n < 0) throw new Exception("Error index");

                    int val = 2;
                    int cnt = 0;

                    while (cnt != n)
                    {
                        val++;
                        if (IsPrime(val)) cnt++;
                    }

                    return val;
                }
            }

            private bool IsPrime(int val)
            {
                for (int i = 2; i < val; i++)
                {
                    if (val % i == 0)
                        return false;
                }

                return true;
            }
        }

        #endregion

        #region For Task2

        class FileWords
        {
            private string path;

            public FileWords(string path)
            {
                if (!(new FileInfo(path).Exists))
                    throw new Exception("Error file path");

                this.path = path;
            }

            public string this[int index]
            {
                get
                {
                    if (index < 0) throw new Exception("Error index");
                    string str = File.ReadAllText(path);
                    var words = str.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    if(index >= words.Length) throw new Exception("Error index");

                    return words[index];
                }
                set
                {
                    if (index < 0) throw new Exception("Error index");
                    string str = File.ReadAllText(path);
                    var words = str.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    if (index >= words.Length) throw new Exception("Error index");

                    words[index] = value;

                    File.WriteAllLines(path, words);
                }
            }

            public int Length
            {
                get
                {
                    return File.ReadAllText(path).Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
        }

        #endregion
    }
}
