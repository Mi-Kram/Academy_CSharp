using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Tasks
    {
        static public void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Пользователь вводит имя файла, программа подсчитывает сумму всех целых ");
            Console.WriteLine("       чисел, которые являются отдельными словами в файле и выводит на экран  ");
            Console.WriteLine("       C:\\Users\\Михаил\\source\\repos\\03_Kramarenko\\Main\\Task1\\Sum.txt          \n\n");
            Console.ResetColor();

            try
            {
                Console.Write("Имя файла> ");
                string fName = Console.ReadLine(); // C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task1\Sum.txt
                Console.WriteLine("\n\n");

                string text = File.ReadAllText(fName);
                string[] words = text.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int sum = 0, num = 0;

                int cnt = 0;
                foreach (var item in words) {
                    if (Int32.TryParse(item, out num))
                    {
                        sum += num;
                        Console.WriteLine($"{++cnt}. {num}");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSum = {sum}");
                Console.ResetColor();

                Console.WriteLine("\n\n\nПосмотреть содержимое файла (нажмите space)");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("\n\n");

                    Console.WriteLine(File.ReadAllText(fName));

                    Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                    Console.ReadLine();
                }

            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {e.Message}\b!");
                Console.ResetColor();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadLine();
            }

            Console.Clear();
        }
        static public void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. Пользователь вводит 2 имени файла. Программа копирует из первого файла во второй только   ");
            Console.WriteLine("       имена людей можно с повторениями, список которых нахдится в 3 файле, записанный в столбик ");
            Console.WriteLine(@"       Первый:     C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\text.txt                ");
            Console.WriteLine(@"       Второй:     C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\result.txt              ");
            Console.WriteLine(@"       Третий:     C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\allowedNames.txt        ");
            Console.WriteLine("\n\n\n");
            Console.ResetColor();

            try
            {
                //string FirstFile = @"C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\text.txt";
                //Console.WriteLine($"Путь к первому файлу>  {FirstFile}");
                Console.Write("Путь к первому файлу>  ");
                string FirstFile = Console.ReadLine(); // C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\text.txt
                string text = File.ReadAllText(FirstFile);
                string[] words = text.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 0) throw new Exception("Файл с текстом пуст.");

                //string SecondFile = @"C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\result.txt";
                //Console.WriteLine($"Путь ко второму файлу> {SecondFile}");
                Console.Write("Путь ко второму файлу> ");
                string SecondFile = Console.ReadLine(); // C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\result.txt
                StreamWriter sw = new StreamWriter(SecondFile, false, Encoding.Default);

                //string ThirdFile = @"C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\allowedNames.txt";
                //Console.WriteLine($"Путь к третьему файлу> {ThirdFile}");
                Console.Write("Путь к третьему файлу> ");
                string ThirdFile = Console.ReadLine(); // C:\Users\Михаил\source\repos\03_Kramarenko\Main\Task2\allowedNames.txt
                string[] allowedNames = File.ReadAllLines(ThirdFile);
                if (allowedNames.Length == 0) throw new Exception("Файл с разрешенными именами пуст.");
                Console.WriteLine("\n\n");

                bool[] IsFind = new bool[allowedNames.Length];
                
                foreach (var item in words)
                {
                    for (int i = 0; i < allowedNames.Length; i++)
                    {
                        if(item == allowedNames[i])
                        {
                            if (!IsFind[i])
                            {
                                IsFind[i] = true;
                                sw.WriteLine(item);
                            }
                            else break;
                        }
                    }
                }

                sw.Close();

                Console.Write("\t\t\t\t\t");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Task2 завершил работу");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {e.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        static public void Task3()
        {
            #region Task3 с плавающей точкой

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task3. Пользователь вводит строку вида: 23+ 34 -34 * 123. Программа ");
            Console.WriteLine("       подсчитывает результат. Разрешённые операторы: + - / * %     ");
            Console.WriteLine("       5*2 + 1.5 - 1.5 - 20 / 4 | 5 + 4.2.5                         \n\n");
            Console.ResetColor();

            try
            {
                Console.Write("Введите строку> ");
                string str = Console.ReadLine();
                if (str.Length == 0) throw new Exception("Строка пустая");
                string[] AllActions;

                double result = 0, dbl = 0;

                #region DoRightStr

                string rightStr = null;
                bool dig = false;
                bool op = false;
                bool space = false;
                bool punkt = false;

                foreach (var item in str)
                {
                    if (Char.IsDigit(item))
                    {
                        if (dig)
                        {
                            if (space) throw new Exception("Нет оператора между числами");

                            rightStr += item;
                        }
                        else
                        {
                            if (rightStr == null)
                            {
                                rightStr += item;
                            }
                            else if (op)
                            {
                                op = false;
                                if (rightStr.Length > 1) rightStr += ' ';
                                rightStr += item;
                                if (rightStr[0] == '+' && rightStr.Length == 2)
                                {
                                    if (!double.TryParse(rightStr, out dbl)) throw new Exception("Избавление от плюса в начале строки");
                                    rightStr = dbl.ToString();
                                }
                            }
                            else throw new Exception("Первый if в foreach для создания правильной строки");

                            dig = true;
                            if (space) space = false;
                        }
                    }
                    else if (item == '+' || item == '-' || item == '*' || item == '/' || item == '%')
                    {
                        if (rightStr == null)
                        {
                            if (item == '-' || item == '+') rightStr += item;
                            else throw new Exception("Первый символ не может быть подобным знаком (* / %)");
                            op = true;
                            continue;
                        }
                        if (op) throw new Exception("Два знака подряд");
                        if (punkt)
                        {
                            if (rightStr[rightStr.Length - 1] == ',' || rightStr[rightStr.Length - 1] == '.')
                                throw new Exception("Нет цифр после плавающей точки");
                            punkt = false;
                        }

                        op = true;
                        if (space) space = false;
                        if (dig)
                        {
                            dig = false;
                            rightStr += ' ';
                            rightStr += item;
                        }
                        else throw new Exception("Третий if в первом else if в foreach для создания правильной строки");
                    }
                    else if (item == ',' || item == '.')
                    {
                        if (rightStr == null) throw new Exception("Нет цифр перед плавающей точки");
                        if (space || op) throw new Exception("Нет цифр передплавающей точки");
                        if (punkt) throw new Exception("В одном числе может быть только 1 плавающая точка");
                        if (dig)
                        {
                            punkt = true;
                            rightStr += ',';
                        }
                    }
                    else if (item == ' ')
                    {
                        if (dig || op) space = true;
                        if (punkt)
                        {
                            if (rightStr[rightStr.Length - 1] == ',' || rightStr[rightStr.Length - 1] == '.')
                                throw new Exception("Нет цифр после плавающей точки");
                            punkt = false;
                        }
                    }
                    else throw new Exception("Строка заполнена неверно");
                }
                if (op) throw new Exception("В конце после знака нет числа");
                if (punkt)
                {
                    if (rightStr[rightStr.Length - 1] == ',' || rightStr[rightStr.Length - 1] == '.')
                        throw new Exception("Нет цифр после плавающей точки");
                }

                #endregion

                #region Избавление от приоритетных операций

                string[] actions = rightStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                AllActions = new string[actions.Length / 2 + 1];
                if (AllActions.Length == 1)
                {
                    if (!double.TryParse(rightStr, out dbl)) throw new Exception("Избавление от приоритетных операций (В строке одно число)");
                    rightStr = dbl.ToString();
                }
                str = rightStr;
                int cnt = 0;

                while (true)
                {
                    bool flag = false;
                    AllActions[cnt++] = rightStr;
                    actions = rightStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 1; i < actions.Length; i++)
                    {
                        if (actions[i] == "*" || actions[i] == "/" || actions[i] == "%")
                        {
                            flag = true;
                            if (i == 1)
                            {
                                if (!double.TryParse(actions[0], out result)) throw new Exception("Избавление от приоритетных операций №1");
                                if (!double.TryParse(actions[2], out dbl)) throw new Exception("Избавление от приоритетных операций №2");
                                switch (actions[1])
                                {
                                    case "*": result *= dbl; break;
                                    case "/":
                                        if (dbl == 0) throw new Exception("Деление на нуль");
                                        result /= dbl; break;
                                    case "%":
                                        if (dbl == 0) throw new Exception("Модульное деление на нуль");
                                        result %= dbl; break;
                                    default: throw new Exception("Избавление от приоритетных операций №3");
                                }

                                rightStr = result.ToString();

                                for (int a = 3; a < actions.Length; a++)
                                {
                                    rightStr += ' ';
                                    rightStr += actions[a];
                                }
                            }
                            else
                            {
                                string tmp = actions[i - 2];
                                tmp += actions[i - 1];
                                if ((tmp[0] == '+' || tmp[0] == '-') && (tmp[1] == '+' || tmp[1] == '-'))
                                {
                                    if ((tmp[0] == '+' && tmp[1] == '+') || (tmp[0] == '-' && tmp[1] == '-'))
                                    {
                                        tmp = actions[i - 1];
                                        tmp.Replace('-', '+');
                                    }
                                    else
                                    {
                                        tmp = actions[i - 1];
                                        tmp.Replace('+', '-');
                                    }
                                }
                                if (!double.TryParse(tmp, out result)) throw new Exception("Избавление от приоритетных операций №4");
                                if (!double.TryParse(actions[i + 1], out dbl)) throw new Exception("Избавление от приоритетных операций №5");

                                switch (actions[i])
                                {
                                    case "*": result *= dbl; break;
                                    case "/":
                                        if (dbl == 0) throw new Exception("Деление на нуль");
                                        result /= dbl; break;
                                    case "%":
                                        if (dbl == 0) throw new Exception("Модульное деление на нуль");
                                        result %= dbl; break;
                                    default: throw new Exception("Избавление от приоритетных операций №6");
                                }

                                rightStr = actions[0];

                                for (int a = 1; a < i - 2; a++)
                                {
                                    rightStr += ' ';
                                    rightStr += actions[a];
                                }

                                rightStr += ' ';
                                if (result < 0) rightStr += '-';
                                else rightStr += '+';
                                rightStr += ' ';
                                if (result < 0) result = -result;
                                rightStr += result.ToString();

                                for (int a = i + 2; a < actions.Length; a++)
                                {
                                    rightStr += ' ';
                                    rightStr += actions[a];
                                }
                            }
                            break;
                        }
                    }
                    if (!flag) break;
                }

                #endregion

                #region Избавление от '+' и '-'

                if (!Double.TryParse(actions[0], out result)) throw new Exception("Избавление от '+' и '-' №1");
                for (int i = 2; i < actions.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        switch (actions[i - 1])
                        {
                            case "+":
                                if (!double.TryParse(actions[i], out dbl)) throw new Exception("Избавление от '+' и '-' №2");
                                result += dbl;
                                break;
                            case "-":
                                if (!double.TryParse(actions[i], out dbl)) throw new Exception("Избавление от '+' и '-' №3");
                                result -= dbl;
                                break;
                            default: throw new Exception("Избавление от '+' и '-' №4");
                        }

                        AllActions[cnt] = $"{result}";
                        if (i != actions.Length - 1) AllActions[cnt] += $" {actions[i + 1]}";
                        for (int a = i + 2; a < actions.Length; a++)
                        {
                            AllActions[cnt] += ' ';
                            AllActions[cnt] += actions[a];
                        }
                        cnt++;

                    }
                }

                #endregion


                Console.WriteLine($"\n\n\n{str} = {result:0.###}");

                Console.WriteLine("\n\n\nПосмотреть очерёдность выполнения операций (нажмите space)");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("\n\n");
                    foreach (var item in AllActions)
                    {
                        if (item == null) break;
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {ex.Message}!");
                Console.ResetColor();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadLine();
            }

            Console.Clear();

            #endregion


            #region Task3 без плавающей точки

            //Console.BackgroundColor = ConsoleColor.Gray;
            //Console.ForegroundColor = ConsoleColor.Black;
            //Console.WriteLine("Task3. Пользователь вводит строку вида: 23+ 34 -34 * 123. Программа ");
            //Console.WriteLine("       подсчитывает результат. Разрешённые операторы: + - / * %     \n\n");
            //Console.ResetColor();

            //try
            //{
            //    Console.Write("Введите строку> ");
            //    string str = Console.ReadLine();
            //    if (str.Length == 0) throw new Exception("Строка пустая");
            //    string[] AllActions;

            //    double result = 0, dbl = 0;

            //    #region DoRightStr

            //    string rightStr = null;
            //    bool dig = false;
            //    bool op = false;
            //    bool space = false;

            //    foreach (var item in str)
            //    {
            //        if (Char.IsDigit(item))
            //        {
            //            if (dig)
            //            {
            //                if (space) throw new Exception("Нет оператора между числами");

            //                rightStr += item;
            //            }
            //            else
            //            {
            //                if (rightStr == null)
            //                {
            //                    rightStr += item;
            //                }
            //                else if (op)
            //                {
            //                    op = false;
            //                    if (rightStr.Length > 1) rightStr += ' ';
            //                    rightStr += item;
            //                    if (rightStr[0] == '+' && rightStr.Length == 2)
            //                    {
            //                        if (!double.TryParse(rightStr, out dbl)) throw new Exception("Избавление от плюса в начале строки");
            //                        rightStr = dbl.ToString();
            //                    }
            //                }
            //                else throw new Exception("Первый if в foreach для создания правильной строки");

            //                dig = true;
            //                if (space) space = false;
            //            }
            //        }
            //        else if (item == '+' || item == '-' || item == '*' || item == '/' || item == '%')
            //        {
            //            if (rightStr == null)
            //            {
            //                if (item == '-' || item == '+') rightStr += item;
            //                else throw new Exception("Первый символ не может быть подобным знаком (* / %)");
            //                op = true;
            //                continue;
            //            }
            //            if (op) throw new Exception("Два знака подряд");

            //            op = true;
            //            if (space) space = false;
            //            if (dig)
            //            {
            //                dig = false;
            //                rightStr += ' ';
            //                rightStr += item;
            //            }
            //            else throw new Exception("Третий if в первом else if в foreach для создания правильной строки");
            //        }
            //        else if (item == ' ')
            //        {
            //            if (dig || op) space = true;
            //        }
            //        else throw new Exception("Строка заполнена неверно");
            //    }
            //    if (op) throw new Exception("В конце после знака нет числа");
            //    str = rightStr;

            //    #endregion

            //    #region Избавление от приоритетных операций

            //    string[] actions = rightStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //    AllActions = new string[actions.Length / 2 + 1];
            //    int cnt = 0;

            //    while (true)
            //    {
            //        bool flag = false;
            //        AllActions[cnt++] = rightStr;
            //        actions = rightStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //        for (int i = 1; i < actions.Length; i++)
            //        {
            //            if (actions[i] == "*" || actions[i] == "/" || actions[i] == "%")
            //            {
            //                flag = true;
            //                if (i == 1)
            //                {
            //                    if (!double.TryParse(actions[0], out result)) throw new Exception("Избавление от приоритетных операций №1");
            //                    if (!double.TryParse(actions[2], out dbl)) throw new Exception("Избавление от приоритетных операций №2");
            //                    switch (actions[1])
            //                    {
            //                        case "*": result *= dbl; break;
            //                        case "/":
            //                            if (dbl == 0) throw new Exception("Деление на нуль");
            //                            result /= dbl; break;
            //                        case "%":
            //                            if (dbl == 0) throw new Exception("Модульное деление на нуль");
            //                            result %= dbl; break;
            //                        default: throw new Exception("Избавление от приоритетных операций №3");
            //                    }

            //                    rightStr = result.ToString();

            //                    for (int a = 3; a < actions.Length; a++)
            //                    {
            //                        rightStr += ' ';
            //                        rightStr += actions[a];
            //                    }
            //                }
            //                else
            //                {
            //                    string tmp = actions[i - 2];
            //                    tmp += actions[i - 1];
            //                    if ((tmp[0] == '+' || tmp[0] == '-') && (tmp[1] == '+' || tmp[1] == '-'))
            //                    {
            //                        if ((tmp[0] == '+' && tmp[1] == '+') || (tmp[0] == '-' && tmp[1] == '-'))
            //                        {
            //                            tmp = actions[i - 1];
            //                            tmp.Replace('-', '+');
            //                        }
            //                        else
            //                        {
            //                            tmp = actions[i - 1];
            //                            tmp.Replace('+', '-');
            //                        }
            //                    }
            //                    if (!double.TryParse(tmp, out result)) throw new Exception("Избавление от приоритетных операций №4");
            //                    if (!double.TryParse(actions[i + 1], out dbl)) throw new Exception("Избавление от приоритетных операций №5");

            //                    switch (actions[i])
            //                    {
            //                        case "*": result *= dbl; break;
            //                        case "/":
            //                            if (dbl == 0) throw new Exception("Деление на нуль");
            //                            result /= dbl; break;
            //                        case "%":
            //                            if (dbl == 0) throw new Exception("Модульное деление на нуль");
            //                            result %= dbl; break;
            //                        default: throw new Exception("Избавление от приоритетных операций №6");
            //                    }

            //                    rightStr = actions[0];

            //                    for (int a = 1; a < i - 2; a++)
            //                    {
            //                        rightStr += ' ';
            //                        rightStr += actions[a];
            //                    }

            //                    rightStr += ' ';
            //                    if (result < 0) rightStr += '-';
            //                    else rightStr += '+';
            //                    rightStr += ' ';
            //                    if (result < 0) result = -result;
            //                    rightStr += result.ToString();

            //                    for (int a = i + 2; a < actions.Length; a++)
            //                    {
            //                        rightStr += ' ';
            //                        rightStr += actions[a];
            //                    }
            //                }
            //                break;
            //            }
            //        }
            //        if (!flag) break;
            //    }

            //    #endregion

            //    #region Избавление от '+' и '-'

            //    if (!Double.TryParse(actions[0], out result)) throw new Exception("Избавление от '+' и '-' №1");
            //    for (int i = 2; i < actions.Length; i++)
            //    {
            //        if (i % 2 == 0)
            //        {
            //            switch (actions[i - 1])
            //            {
            //                case "+":
            //                    if (!double.TryParse(actions[i], out dbl)) throw new Exception("Избавление от '+' и '-' №2");
            //                    result += dbl;
            //                    break;
            //                case "-":
            //                    if (!double.TryParse(actions[i], out dbl)) throw new Exception("Избавление от '+' и '-' №3");
            //                    result -= dbl;
            //                    break;
            //                default: throw new Exception("Избавление от '+' и '-' №4");
            //            }

            //            AllActions[cnt] = $"{result}";
            //            if (i != actions.Length - 1) AllActions[cnt] += $" {actions[i + 1]}";
            //            for (int a = i + 2; a < actions.Length; a++)
            //            {
            //                AllActions[cnt] += ' ';
            //                AllActions[cnt] += actions[a];
            //            }
            //            cnt++;

            //        }
            //    }

            //    #endregion


            //    Console.WriteLine($"\n\n\n{str} = {result:0.###}");

            //    Console.WriteLine("\n\n\nПосмотреть очерёдность выполнения операций (нажмите space)");
            //    if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            //    {
            //        Console.WriteLine("\n\n");
            //        foreach (var item in AllActions)
            //        {
            //            if (item == null) break;
            //            Console.WriteLine(item);
            //        }
            //        Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            //        Console.ReadLine();
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Console.Clear();
            //    Console.BackgroundColor = ConsoleColor.DarkRed;
            //    Console.WriteLine($"Task3 не выполнен. {ex.Message}!");
            //    Console.ResetColor();
            //    Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            //    Console.ReadLine();
            //}

            //Console.Clear();

            #endregion
        }
    }
}
