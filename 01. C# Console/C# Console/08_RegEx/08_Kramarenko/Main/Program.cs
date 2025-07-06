using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            Console.WriteLine("Task1. Разработать регулярные выражения, позволяющие детектировать следующие шаблоны: ");
            Console.WriteLine("                                                                                      ");
            Console.WriteLine("       (xxx)  xxx-xx-xx                                                               ");
            Console.WriteLine("       12.02.1997 (в месяце - 31 день, диапазон годов: 1900-2100)                     ");
            Console.WriteLine("       123 кг 789 г                                                                   ");
            Console.WriteLine("       дробные числа: -123.34, 34, 0.23, .45, -.34                                    \n\n\n");
            Console.ResetColor();

            try
            {
                RegEx("a)", @"^\(\d\d\d\)\s+\d\d\d\-\d\d\-\d\d$");
                RegEx("b)", @"^(0[1-9]|[12][1-9]|3[01])\.(0[1-9]|1[12])\.(19\d\d|2(0\d\d|100))$");
                RegEx("c)", @"((^\d+\s+кг$)|(^([1-9]|(0[1-9]|[1-9]\d)|(00[1-9]|0[1-9]\d|[1-9]\d\d))\s+г$)|(^\d+\s+кг\s+([1-9]|(0[1-9]|[1-9]\d)|(00[1-9]|0[1-9]\d|[1-9]\d\d))\s+г$))");
                RegEx("d)" , @"^\-?(\.\d+|\d+(\.\d+)?)$");

                Console.WriteLine("\n\nTask1 Выполнен");
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
            Console.WriteLine("Task2. Пользователь вводит имя файла, содержащего текст, содержащий арифметические выражения     ");
            Console.WriteLine("       вида 123+45. Разрешённые операторы: +, -, *, /, %. Программа находит, используя RegEx все ");
            Console.WriteLine("       выражения, вычисляет их и результат помещает в результирующий файл вместо примеров        \n\n\n");
            Console.ResetColor();

            try
            {
                string result = @"C:\Users\Михаил\source\repos\08_Kramarenko\Main\result.txt";
                
                string path = @"C:\Users\Михаил\source\repos\08_Kramarenko\Main\text.txt";
                Console.WriteLine($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();

                string str = File.ReadAllText(path);
                Console.WriteLine($"\n\nИсходный текст:\n{str}");

                DisposeOP(ref str);

                Console.WriteLine($"\n\nРезультирующий текст:\n{str}");
                File.WriteAllText(result, str);

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

        #region For Task1

        /// <summary>
        /// детектор шаблонов pattern
        /// </summary>
        private static void RegEx(string number, string pattern)
        {
            string str;
            MatchCollection m;

            try
            {
                Console.Write($"{number} ");
                str = Console.ReadLine();
                m = Regex.Matches(str, pattern, RegexOptions.IgnoreCase);

                if (m.Count == 1) Console.WriteLine("Хорошо\n");
                else throw new Exception("Неверно\n");
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }

        #endregion

        #region For Task2

        /// <summary>
        /// решение примеров в строке str (приоритеты учитываются)
        /// </summary>
        /// <param name="str"></param>
        private static void DisposeOP(ref string str)
        {
            MatchCollection m1;
            MatchCollection m2;

            // избавление строки str от примеров со знаком * и /
            while (true)
            {
                // получение примеров со знаком * и /
                m1 = Regex.Matches(str, @"\d+(\.\d+)?\s+\*\s+\d+(\.\d+)?", RegexOptions.IgnoreCase);
                m2 = Regex.Matches(str, @"\d+(\.\d+)?\s+\/\s+\d+(\.\d+)?", RegexOptions.IgnoreCase);

                // выяснение какая операция будет вычисляться первой
                int n1, n2;
                n1 = n2 = str.Length;
                if (m1.Count != 0) n1 = m1[0].Index;
                if (m2.Count != 0) n2 = m2[0].Index;
                if (n1 == n2) break;

                // true  - *
                // false - /
                bool op = n1 < n2 ? true : false;

                if (op)
                {
                    // получение примера в виде массива
                    // [0] число
                    // [1] оператор
                    // [2] число
                    string[] task = (str.Remove(0, m1[0].Index).Remove(m1[0].Length, str.Length - (m1[0].Length + m1[0].Index))).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (task.Length != 3) throw new Exception("What's going wrong");

                    // решение примера
                    int a = int.Parse(task[0]);
                    int b = int.Parse(task[2]);
                    int res = a * b;

                    // замена примера на ответ
                    str = str.Replace(m1[0].Value, res.ToString());
                }
                else
                {
                    // получение примера в виде массива
                    // [0] число
                    // [1] оператор
                    // [2] число
                    string[] task = (str.Remove(0, m2[0].Index).Remove(m2[0].Length, str.Length - (m2[0].Length + m2[0].Index))).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (task.Length != 3) throw new Exception("What's going wrong");

                    // решение примера
                    int a = int.Parse(task[0]);
                    int b = int.Parse(task[2]);
                    int res = a / b;

                    // замена примера на ответ
                    str = str.Replace(m2[0].Value, res.ToString());
                }
            }

            // избавление строки str от примеров со знаком + и -
            while (true)
            {
                // получение примеров со знаком + и -
                m1 = Regex.Matches(str, @"\d+(\.\d+)?\s+\+\s+\d+(\.\d+)?", RegexOptions.IgnoreCase);
                m2 = Regex.Matches(str, @"\d+(\.\d+)?\s+\-\s+\d+(\.\d+)?", RegexOptions.IgnoreCase);

                // выяснение какая операция будет вычисляться первой
                int n1, n2;
                n1 = n2 = str.Length;
                if (m1.Count != 0) n1 = m1[0].Index;
                if (m2.Count != 0) n2 = m2[0].Index;
                if (n1 == n2) break;

                // true  - *
                // false - /
                bool op = n1 < n2 ? true : false;

                if (op)
                {
                    // получение примера в виде массива
                    // [0] число
                    // [1] оператор
                    // [2] число
                    string[] task = (str.Remove(0, m1[0].Index).Remove(m1[0].Length, str.Length - (m1[0].Length + m1[0].Index))).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (task.Length != 3) throw new Exception("What's going wrong");

                    // решение примера
                    int a = int.Parse(task[0]);
                    int b = int.Parse(task[2]);
                    int res = a + b;

                    // замена примера на ответ
                    str = str.Replace(m1[0].Value, res.ToString());
                }
                else
                {
                    // получение примера в виде массива
                    // [0] число
                    // [1] оператор
                    // [2] число
                    string[] task = (str.Remove(0, m2[0].Index).Remove(m2[0].Length, str.Length - (m2[0].Length + m2[0].Index))).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (task.Length != 3) throw new Exception("What's going wrong");

                    // решение примера
                    int a = int.Parse(task[0]);
                    int b = int.Parse(task[2]);
                    int res = a - b;

                    // замена примера на ответ
                    str = str.Replace(m2[0].Value, res.ToString());
                }
            }
        }

        #endregion
    }
}
